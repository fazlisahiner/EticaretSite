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
    public class AddressController : ControllerBase
    {
        private readonly string _connectionString;

        public AddressController()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-address") ]
        public async Task<IActionResult> GetAddresses()
        {
            try
            {
                using (var conn =new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM addresses";
                    using ( var cmd = new MySqlCommand(query, conn))
                    {
                        using(var reader = await cmd.ExecuteReaderAsync())
                        {
                            var addresses = new List<Address>();
                            while(await reader.ReadAsync()) 
                            {
                                var address = new Address
                                {
                                    AddressId = Convert.ToInt32(reader["AddressID"]),
                                    UserId = Convert.ToInt32(reader["UserID"]),
                                    CountryId = Convert.ToInt32(reader["CountryID"]),
                                    CityId = Convert.ToInt32(reader["CityID"]),
                                    TownId = Convert.ToInt32(reader["TownID"]),
                                    DistrictId = Convert.ToInt32(reader["DistrictID"]),
                                    NeighbourhoodId = Convert.ToInt32(reader["NeighborhoodID"]),
                                    AddressText = reader["Address"].ToString()
                                };
                               addresses.Add(address);
                            }
                            return Ok(addresses);
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
