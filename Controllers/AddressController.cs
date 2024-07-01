using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using EticaretSite.Models;
using Mysqlx;
//using ETicaret.Core.ETicaretModel;

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

        [HttpGet]
        [Route("get-address-by-user/{UserId}")]
        public async Task<IActionResult> GetAddressByUser(int UserId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM addresses Where UserID =@UserId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        using ( var reader = await cmd.ExecuteReaderAsync())
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

        [HttpPost]
        [Route("add-address")]
        public async Task<IActionResult> AddAddress([FromBody] Address address)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "INSERT INTO addresses(UserID, CountryID, CityID, TownID, DistrictID, NeighborhoodID, Address ) VALUES (@UserID, @CountryID, @CityID, @TownID, @DistrictID, @NeighborhoodID, @Address)";
                    using (var cmd = new MySqlCommand(query, conn)) 
                    {
                        cmd.Parameters.AddWithValue("@UserID", address.UserId);
                        cmd.Parameters.AddWithValue("@CountryID", address.CountryId);
                        cmd.Parameters.AddWithValue("@CityID", address.CityId);
                        cmd.Parameters.AddWithValue("@TownID", address.TownId);
                        cmd.Parameters.AddWithValue("@DistrictID", address.DistrictId);
                        cmd.Parameters.AddWithValue("@NeighborhoodID", address.NeighbourhoodId);
                        cmd.Parameters.AddWithValue("@Address", address.AddressText);
                        await cmd.ExecuteNonQueryAsync();
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete-address/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "DELETE FROM addresses WHERE AddressID = @AddressID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AddressID", addressId);
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

        [HttpPut]
        [Route("update-address/{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, [FromBody] Address updateAddress)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE addresses SET TownID = @TownID, DistrictID=@DistrictID, NeighborhoodID=@NeighbourhoodID, Address=@Address WHERE AddressID=@AddressID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TownID", updateAddress.TownId);
                        cmd.Parameters.AddWithValue("@DistrictID", updateAddress.DistrictId);
                        cmd.Parameters.AddWithValue("@NeighbourhoodID", updateAddress.NeighbourhoodId);
                        cmd.Parameters.AddWithValue("@Address", updateAddress.AddressText);
                        cmd.Parameters.AddWithValue("@AddressID", addressId);
                        
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
