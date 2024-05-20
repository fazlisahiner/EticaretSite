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
    public class EmplooyeeController: ControllerBase
    {
        private readonly string _connectionString;
        public EmplooyeeController()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
            
        }
        [HttpGet]
        [Route("get-emplooyee")]
        public async Task<IActionResult> GetEmplooyee()
        {
            try 
            {
                using ( var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM employee";
                    using (var cmd = new MySqlCommand(query,conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var emplooyees = new List<Emplooyee>();
                            while ( await reader.ReadAsync() )
                            {
                                var emplooyee = new Emplooyee
                                {
                                    EmplooyeeId = Convert.ToInt32(reader ["EmployeID"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    BirthDate =  Convert.ToDateTime(reader["BirthDate"]),
                                    TelNumber1 = reader["TelNumber1"].ToString(),
                                    CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                                    HireDate = Convert.ToDateTime(reader["HireDate"]),
                                    Position = reader["Position"].ToString(),
                                    CountryId = Convert.ToInt32(reader["CountryID"]),
                                    CityId = Convert.ToInt32(reader["CityID"]),
                                    TownId = Convert.ToInt32(reader["TownID"]),
                                    DistrictId = Convert.ToInt32(reader["DistrictID"]),
                                    NeighbourhoodId = Convert.ToInt32(reader["NeighborhoodID"]),
                                    AddressText = reader["AddressText"].ToString()
                                };

                                emplooyees.Add(emplooyee);
                            };
                            return Ok(emplooyees);
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