using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using EticaretSite.Models;

namespace EticaretSite.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly string _connectionString;
        public DistrictController()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-district")]
        public async Task<IActionResult> GetDistricts()
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM district";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var districts = new List<District>();
                            while (await reader.ReadAsync())
                            {
                                var district = new District
                                {
                                    DistrictId = Convert.ToInt32(reader["DistrictID"]),
                                    TownId = Convert.ToInt32(reader["TownID"]),
                                    DistrictName = reader["DistrictName"].ToString()
                                };
                                districts.Add(district);
                            }
                            return Ok(districts);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("update-district/{districtId}")]
        public async Task<IActionResult> UpdateDistrict(int districtId, [FromBody] District updatedDistrict)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE district SET TownID=@TownID, DistrictName=@DistrictName WHERE DistrictID=@DistrictID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TownID", updatedDistrict.TownId);
                        cmd.Parameters.AddWithValue("@DistrictName", updatedDistrict.DistrictName);
                        cmd.Parameters.AddWithValue("@DistrictID", districtId);

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

        [HttpGet]
        [Route("search-district")]
        public async Task<IActionResult> SearchDistrict(string districtName)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM district WHERE DistrictName LIKE @DistrictName";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DistrictName", "%" + districtName + "%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var districts = new List<District>();
                            while (await reader.ReadAsync())
                            {
                                var district = new District
                                {
                                    DistrictId = Convert.ToInt32(reader["DistrictID"]),
                                    TownId = Convert.ToInt32(reader["TownID"]),
                                    DistrictName = reader["DistrictName"].ToString()
                                };
                                districts.Add(district);
                            }
                            return Ok(districts);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}