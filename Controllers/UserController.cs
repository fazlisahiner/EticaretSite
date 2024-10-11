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
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM users";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var users = new List<User>();
                            while (await reader.ReadAsync())
                            {
                                var user = new User
                                {
                                    UserId = Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    //Gender = reader["Gender"].ToString(),
                                    Gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : null,
                                    //BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                                    BirthDate = reader["BirthDate"] != DBNull.Value ? Convert.ToDateTime(reader["BirthDate"]) : null,
                                    TelNumber1 = reader["TelNumber1"].ToString(),
                                    //TelNumber2 = reader["TelNumber2"].ToString(),
                                    TelNumber2 = reader["TelNumber2"] != DBNull.Value ? reader["TelNumber2"].ToString() : null,
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

        [HttpGet]
        [Route("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM users WHERE UserId = @UserId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", id);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var user = new User
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    //Gender = reader["Gender"].ToString(),
                                    Gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : null,
                                    //BirthDate = Convert.ToDateTime(reader["BirthDate"]),

                                    BirthDate = reader["BirthDate"] != DBNull.Value ? Convert.ToDateTime(reader["BirthDate"]) : null,
                                    TelNumber1 = reader["TelNumber1"].ToString(),
                                    // TelNumber2 = reader["TelNumber2"].ToString(),
                                    TelNumber2 = reader["TelNumber2"] != DBNull.Value ? reader["TelNumber2"].ToString() : null,
                                    CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                                    IsActive = Convert.ToInt32(reader["IsActive"]),
                                    Password = reader["Password"].ToString(),
                                };
                                return Ok(user);
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
        [Route("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = @"UPDATE users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Gender = @Gender,  TelNumber1 = @TelNumber1, TelNumber2 = @TelNumber2, 
                               Password = @Password WHERE UserID = @UserId";
                    //IsActive = @IsActive, CreateDate = @CreateDate, BirthDate = @BirthDate 

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", id);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Gender", user.Gender ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TelNumber1", user.TelNumber1);
                        cmd.Parameters.AddWithValue("@TelNumber2", user.TelNumber2 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Password", user.Password);

                        //cmd.Parameters.AddWithValue("@BirthDate", user.BirthDate ?? (object)DBNull.Value);
                        //cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                        //cmd.Parameters.AddWithValue("@CreateDate", user.CreateDate);

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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    // Update query to set IsActive to 0 instead of deleting
                    string query = "UPDATE users SET IsActive = 0 WHERE UserID = @UserId";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", id);

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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}