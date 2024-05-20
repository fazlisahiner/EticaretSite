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
        public TownController ()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-town")]
        public async Task<IActionResult> GetTown()
        {
            try 
            {
                using(var conn= new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM town";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using(var reader = await cmd.ExecuteReaderAsync())
                        {
                            var towns = new List<Town>();
                            while ( await reader.ReadAsync() )
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
                return StatusCode ( 500, ex.Message );
            }
        }

    }
}