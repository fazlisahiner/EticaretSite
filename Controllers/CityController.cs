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
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly string _connectionString;
        public CityController()
        {
            _connectionString= "server=localhost; database=eticaretsite; user=root; password=";
        }
        [HttpGet]
        [Route("get-city")]
        public async Task<IActionResult> GetCity()
        {
            try
            {
                using(var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM city";
                    using ( var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var cities= new List<City>();
                            while ( await reader.ReadAsync() )
                            {
                                var city = new City
                                {
                                    CityId = Convert.ToInt32(reader["CityID"]),
                                    CountryId = Convert.ToInt32(reader["CountryID"]),
                                    CityName = reader["CityName"].ToString(),
                                    PlateNo = reader["PlateNo"].ToString(),
                                    PhoneCode = reader["PhoneCode"].ToString()

                                };
                                cities.Add(city);
                            }
                            return Ok(cities);
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