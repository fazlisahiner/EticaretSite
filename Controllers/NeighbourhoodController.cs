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
        public async Task<IActionResult> GetNeighbourhoods ()
        {
            try 
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM neighborhood";
                    using ( var cmd = new MySqlCommand(query, conn))
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
                return StatusCode (500, error.Message);
            }
        }

    }
}