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
    public class EmplooyeeController : ControllerBase
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
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM employee";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var emplooyees = new List<Emplooyee>();
                            while (await reader.ReadAsync())
                            {
                                var emplooyee = new Emplooyee
                                {
                                    EmplooyeeId = Convert.ToInt32(reader["EmployeID"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    BirthDate = Convert.ToDateTime(reader["BirthDate"]),
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
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut]
        [Route("update-emplooyee/{emplooyeeId}")]
        public async Task<IActionResult> UpdateEmplooyee(int emplooyeeId, [FromBody] Emplooyee updatedEmplooyee)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    //CreateDate = @CreateDate, 
                    string query = "UPDATE employee SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Gender = @Gender, BirthDate = @BirthDate, TelNumber1 = @TelNumber1, HireDate = @HireDate, Position = @Position, CountryID = @CountryID, CityID = @CityID, TownID = @TownID, DistrictID = @DistrictID, NeighborhoodID = @NeighborhoodID, AddressText = @AddressText WHERE EmployeID = @EmplooyeeId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", updatedEmplooyee.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", updatedEmplooyee.LastName);
                        cmd.Parameters.AddWithValue("@Email", updatedEmplooyee.Email);
                        cmd.Parameters.AddWithValue("@Gender", updatedEmplooyee.Gender);
                        cmd.Parameters.AddWithValue("@BirthDate", updatedEmplooyee.BirthDate);
                        cmd.Parameters.AddWithValue("@TelNumber1", updatedEmplooyee.TelNumber1);
                        //cmd.Parameters.AddWithValue("@CreateDate", updatedEmplooyee.CreateDate);
                        cmd.Parameters.AddWithValue("@HireDate", updatedEmplooyee.HireDate);
                        cmd.Parameters.AddWithValue("@Position", updatedEmplooyee.Position);
                        cmd.Parameters.AddWithValue("@CountryID", updatedEmplooyee.CountryId);
                        cmd.Parameters.AddWithValue("@CityID", updatedEmplooyee.CityId);
                        cmd.Parameters.AddWithValue("@TownID", updatedEmplooyee.TownId);
                        cmd.Parameters.AddWithValue("@DistrictID", updatedEmplooyee.DistrictId);
                        cmd.Parameters.AddWithValue("@NeighborhoodID", updatedEmplooyee.NeighbourhoodId);
                        cmd.Parameters.AddWithValue("@AddressText", updatedEmplooyee.AddressText);
                        cmd.Parameters.AddWithValue("@EmplooyeeId", emplooyeeId);

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

        [HttpGet]
        [Route("search-emplooyee")]
        public async Task<IActionResult> SearchEmplooyee(string searchTerm)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM employee WHERE FirstName LIKE @SearchTerm OR LastName LIKE @SearchTerm";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var emplooyees = new List<Emplooyee>();
                            while (await reader.ReadAsync())
                            {
                                var emplooyee = new Emplooyee
                                {
                                    EmplooyeeId = Convert.ToInt32(reader["EmployeID"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    BirthDate = Convert.ToDateTime(reader["BirthDate"]),
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
                            }
                            return Ok(emplooyees);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("add-emplooyee")]
        public async Task<IActionResult> AddEmplooyee([FromBody] Emplooyee newEmplooyee)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "INSERT INTO employee (FirstName, LastName, Email, Gender, BirthDate, TelNumber1, CreateDate, HireDate, Position, CountryID, CityID, TownID, DistrictID, NeighborhoodID, AddressText) VALUES (@FirstName, @LastName, @Email, @Gender, @BirthDate, @TelNumber1, @CreateDate, @HireDate, @Position, @CountryID, @CityID, @TownID, @DistrictID, @NeighborhoodID, @AddressText)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", newEmplooyee.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", newEmplooyee.LastName);
                        cmd.Parameters.AddWithValue("@Email", newEmplooyee.Email);
                        cmd.Parameters.AddWithValue("@Gender", newEmplooyee.Gender);
                        cmd.Parameters.AddWithValue("@BirthDate", newEmplooyee.BirthDate);
                        cmd.Parameters.AddWithValue("@TelNumber1", newEmplooyee.TelNumber1);
                        cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@HireDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Position", newEmplooyee.Position);
                        cmd.Parameters.AddWithValue("@CountryID", newEmplooyee.CountryId);
                        cmd.Parameters.AddWithValue("@CityID", newEmplooyee.CityId);
                        cmd.Parameters.AddWithValue("@TownID", newEmplooyee.TownId);
                        cmd.Parameters.AddWithValue("@DistrictID", newEmplooyee.DistrictId);
                        cmd.Parameters.AddWithValue("@NeighborhoodID", newEmplooyee.NeighbourhoodId);
                        cmd.Parameters.AddWithValue("@AddressText", newEmplooyee.AddressText);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                        {
                            return Ok();
                        }
                        else
                        {
                            return BadRequest();
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
        [Route("delete-employee/{employeeId}")]
        public async Task<IActionResult> DeleteEmplooyee(int employeeId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    // Foreign key değerlerini null yapın
                    var updateQuery = "UPDATE orders SET EmployeID = NULL WHERE EmployeID = @EmployeeId";
                    using (var updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        await updateCmd.ExecuteNonQueryAsync();
                    }

                    // Ana kaydı silin
                    string query = "DELETE FROM employee WHERE EmployeID = @EmployeeId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

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