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
    public class UserController : ControllerBase
    {

        private readonly string _connectionString;

        public UserController()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-user")]
        public async Task<IActionResult> GetUsers ()
        {
            try
            {
                using (var conn = new MySqlConnection (_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM users";
                    using ( var cmd = new MySqlCommand(query, conn))
                    {
                        using ( var reader = await cmd.ExecuteReaderAsync())
                        {
                            var users = new List<User>();
                            while(await reader.ReadAsync())
                            {
                                var user = new User
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    BirthDate =  Convert.ToDateTime(reader["BirthDate"]),
                                    TelNumber1 = reader["TelNumber1"].ToString(),
                                    TelNumber2 = reader["TelNumber2"].ToString(),
                                    CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                                    IsActive = Convert.ToInt32(reader["IsActive"]),
                                    Password = reader["Password"].ToString(),
                                };
                                users.Add(user);
                            }
                            return Ok(users);

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