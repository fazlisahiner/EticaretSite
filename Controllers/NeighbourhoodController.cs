using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using EticaretSite.Models;
//using MySqlConnector;

namespace EticaretSite.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class NeighbourhoodController : ControllerBase
    {
        private readonly string _connectionString;
        public NeighbourhoodController()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-neighbourhood")]
        public async Task<IActionResult> GetNeighbourhoods()
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM neighborhood";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var neighbourhoods = new List<Neighbourhood>();
                            while (await reader.ReadAsync())
                            {
                                var neighbourhood = new Neighbourhood
                                {
                                    NeighbourhoodId = Convert.ToInt32(reader["NeighborhoodID"]),
                                    DistrictId = Convert.ToInt32(reader["DistrictID"]),
                                    NeighbourhoodName = reader["NeighborhoodName"].ToString(),
                                    ZipCode = reader["ZipCode"].ToString()
                                };
                                neighbourhoods.Add(neighbourhood);
                            }
                            return Ok(neighbourhoods);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }

        [HttpGet]
        [Route("search-neighbourhood")]
        public async Task<IActionResult> SearchNeighbourhood(string neighbourhoodName)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM neighborhood WHERE NeighborhoodName LIKE @NeighbourhoodName";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NeighbourhoodName", "%" + neighbourhoodName + "%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var neighbourhoods = new List<Neighbourhood>();
                            while (await reader.ReadAsync())
                            {
                                var neighbourhood = new Neighbourhood
                                {
                                    NeighbourhoodId = Convert.ToInt32(reader["NeighborhoodID"]),
                                    DistrictId = Convert.ToInt32(reader["DistrictID"]),
                                    NeighbourhoodName = reader["NeighborhoodName"].ToString(),
                                    ZipCode = reader["ZipCode"].ToString()
                                };
                                neighbourhoods.Add(neighbourhood);
                            }
                            return Ok(neighbourhoods);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }

        [HttpPut]
        [Route("update-neighbourhood/{neighbourhoodId}")]
        public async Task<IActionResult> UpdateNeighbourhood(int neighbourhoodId, [FromBody] Neighbourhood updatedNeighbourhood)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE neighborhood SET DistrictID=@DistrictID, NeighborhoodName=@NeighborhoodName, ZipCode=@ZipCode WHERE NeighborhoodID=@NeighborhoodID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DistrictID", updatedNeighbourhood.DistrictId);
                        cmd.Parameters.AddWithValue("@NeighborhoodName", updatedNeighbourhood.NeighbourhoodName);
                        cmd.Parameters.AddWithValue("@ZipCode", updatedNeighbourhood.ZipCode);
                        cmd.Parameters.AddWithValue("@NeighborhoodID", neighbourhoodId);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                        {
                            return Ok();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }

    }
}