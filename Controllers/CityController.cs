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
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }
        [HttpGet]
        [Route("get-city")]
        public async Task<IActionResult> GetCity()
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM city";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var cities = new List<City>();
                            while (await reader.ReadAsync())
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

        [HttpGet]
        [Route("get-city-by-id/{cityId}")]
        public async Task<IActionResult> GetCityById(int cityId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM city where CityID = @CityId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CityId", cityId);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {  /*
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
*/
                            if (await reader.ReadAsync())
                            {
                                var city = new City
                                {
                                    CityId = Convert.ToInt32(reader["CityID"]),
                                    CountryId = Convert.ToInt32(reader["CountryID"]),
                                    CityName = reader["CityName"].ToString(),
                                    PlateNo = reader["PlateNo"].ToString(),
                                    PhoneCode = reader["PhoneCode"].ToString()

                                };
                                return Ok(city);
                            }
                            else
                            {
                                return NotFound();
                            }
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
        [Route("update-city/{cityId}")]
        public async Task<IActionResult> UpdateProduct(int cityId, [FromBody] City updateCity)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE city SET CountryID=@CountryID, CityName = @CityName, PlateNo=@PlateNo, PhoneCode=@PhoneCode WHERE CityID=@CityID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CountryID", updateCity.CountryId);
                        cmd.Parameters.AddWithValue("@CityName", updateCity.CityName);
                        cmd.Parameters.AddWithValue("@PlateNo", updateCity.PlateNo);
                        cmd.Parameters.AddWithValue("@PhoneCode", updateCity.PhoneCode);
                        cmd.Parameters.AddWithValue("@CityID", cityId);

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

        //[Route("search-city-by-name/{cityName}")]
        [HttpGet]
        [Route("search-city")]
        public async Task<IActionResult> SearchCity(string cityName)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM city WHERE CityName LIKE @CityName";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        // % işaretleri ile wildcard arama yapılır
                        cmd.Parameters.AddWithValue("@CityName", "%" + cityName + "%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var cities = new List<City>();
                            while (await reader.ReadAsync())
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
/*
                            if (cities.Any())
                            {
                                return Ok(cities);
                            }
                            else
                            {
                                return NotFound();
                            }*/
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