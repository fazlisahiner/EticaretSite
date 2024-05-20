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
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly string _connectionString;

        public CountryController ( )
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-country")]
        public async Task<IActionResult> GetCountry()
        {
            try 
            {
                using(var conn = new MySqlConnection(_connectionString))
                {
                    await  conn.OpenAsync();
                    string query ="SELECT*FROM country";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var countries = new List<Country>();
                            while(await reader.ReadAsync())
                            {
                                var country = new Country
                                {
                                    CountryId = Convert.ToInt32(reader["CountryID"]),
                                    BinaryCode = reader["BinaryCode"].ToString(),
                                    TripleCode = reader["TripleCode"].ToString(),
                                    CountryName = reader["CountryName"].ToString(),
                                    PhoneCode = reader["PhoneCode"].ToString(),
                                };
                                countries.Add(country);
                            }
                            return Ok(countries);
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