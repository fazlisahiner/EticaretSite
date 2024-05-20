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
                        using ( var reader = await cmd.ExecuteReaderAsync())
                        {
                            var districts = new List<District>();
                            while ( await reader.ReadAsync() )
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
                return StatusCode (500, ex.Message);
            }
        }

    }
}