using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//using MySql.Data.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using EticaretSite.Models;
using Org.BouncyCastle.Crypto.Generators;

namespace EticaretSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly string _connectionString;

        public AuthController()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register( User user)
        {
            try
            {
                using (var conn = new MySqlConnection( _connectionString ))
                {
                    await conn.OpenAsync();
                    string query = @"INSERT INTO users 
                                (FirstName, LastName, Email, Gender, BirthDate, CreateDate, TelNumber1, TelNumber2,   IsActive,  Password) 
                                VALUES 
                                (@FirstName, @LastName, @Email, @Gender, @BirthDate, @CreateDate, @TelNumber1, @TelNumber2,   @IsActive,  @Password)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        // Hash the password before storing it
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);
                        cmd.Parameters.AddWithValue("@TelNumber1", user.TelNumber1);
                        cmd.Parameters.AddWithValue("@TelNumber2", user.TelNumber2 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender", user.Gender ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BirthDate", user.BirthDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                        cmd.Parameters.AddWithValue("@CreateDate", user.CreateDate);

                        await cmd.ExecuteNonQueryAsync();
                        return Ok(new { Message = "User registered successfully" });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM users WHERE Email = @Email";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", user.Email);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string storedPassword = reader["Password"].ToString();
                                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(user.Password, storedPassword);

                                if (isPasswordValid)
                                {
                                    // Here you would typically generate a JWT token or similar for authentication
                                    return Ok(new { Message = "Login successful" });
                                }
                            }
                        }
                    }
                }
                return Unauthorized(new { Message = "Invalid email or password" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
