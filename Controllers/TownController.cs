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
    public class TownController : ControllerBase
    {
        private readonly string _connectionString;
        public TownController()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-town")]
        public async Task<IActionResult> GetTown()
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM town";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var towns = new List<Town>();
                            while (await reader.ReadAsync())
                            {
                                var town = new Town
                                {
                                    TownId = Convert.ToInt32(reader["TownID"]),
                                    CityId = Convert.ToInt32(reader["CityID"]),
                                    TownName = reader["TownName"].ToString()


                                };
                                towns.Add(town);
                            }
                            return Ok(towns);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("search-town")]
        public async Task<IActionResult> SearchTown(string townName)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM town WHERE TownName LIKE @TownName";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TownName", "%" + townName + "%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var towns = new List<Town>();
                            while (await reader.ReadAsync())
                            {
                                var town = new Town
                                {
                                    TownId = Convert.ToInt32(reader["TownID"]),
                                    CityId = Convert.ToInt32(reader["CityID"]),
                                    TownName = reader["TownName"].ToString()
                                };
                                towns.Add(town);
                            }
                            return Ok(towns);
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
        [Route("update-town/{townId}")]
        public async Task<IActionResult> UpdateTown(int townId, [FromBody] Town updatedTown)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE town SET CityID=@CityID, TownName=@TownName WHERE TownID=@TownID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CityID", updatedTown.CityId);
                        cmd.Parameters.AddWithValue("@TownName", updatedTown.TownName);
                        cmd.Parameters.AddWithValue("@TownID", townId);

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