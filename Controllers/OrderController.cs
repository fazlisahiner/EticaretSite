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
    public class OrderController : ControllerBase
    {
        private readonly string _connectionString;
        public OrderController()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-order")]
        public async Task<IActionResult> GetOrder()
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM orders";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var orders = new List<Order>();
                            while (await reader.ReadAsync())
                            {
                                var order = new Order
                                {
                                    OrderId = Convert.ToInt32(reader["OrderID"]),
                                    UserId = Convert.ToInt32(reader["UserID"]),
                                   //UserId = reader["UserID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["UserID"]) : null,
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                    TotalPrice = Convert.ToDecimal(reader["TotalPrice"]),
                                    OrderStatus = Convert.ToInt32(reader["OrderStatus"]),
                                    //EmplooyeeId = Convert.ToInt32(reader["EmployeID"]),
                                  EmployeeId = reader["EmployeID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["EmployeID"]) : null

                                };
                                orders.Add(order);
                            }
                            return Ok(orders);
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
        [Route("get-order-by-id/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM orders WHERE OrderID = @OrderId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var order = new Order
                                {
                                    OrderId = Convert.ToInt32(reader["OrderID"]),
                                    UserId = Convert.ToInt32(reader["UserID"]),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                    TotalPrice = Convert.ToDecimal(reader["TotalPrice"]),
                                    OrderStatus = Convert.ToInt32(reader["OrderStatus"]),
                                    EmployeeId = reader["EmployeID"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["EmployeID"])
                                };
                                return Ok(order);
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

        [HttpPost]
        [Route("add-order")]
        public async Task<IActionResult> AddOrder([FromBody] Order newOrder)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = @"INSERT INTO orders (UserID, OrderDate, TotalPrice, OrderStatus, EmployeID)
                             VALUES (@UserId, @OrderDate, @TotalPrice, @OrderStatus, @EmployeeId);
                             SELECT LAST_INSERT_ID();"; // MySQL'de son eklenen ID'yi almak için
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", newOrder.UserId);
                        cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@TotalPrice", newOrder.TotalPrice);
                        cmd.Parameters.AddWithValue("@OrderStatus", newOrder.OrderStatus);
                        cmd.Parameters.AddWithValue("@EmployeeId", newOrder.EmployeeId ?? (object)DBNull.Value);

                        int orderId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        newOrder.OrderId = orderId;

                        return Ok(newOrder);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("update-order/{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] Order updatedOrder)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = @"UPDATE orders 
                             SET UserId = @UserId,
                                
                                 TotalPrice = @TotalPrice,
                                 OrderStatus = @OrderStatus,
                                 EmployeID = @EmployeeId
                             WHERE OrderID = @OrderId";
                             // OrderDate = @OrderDate,
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", updatedOrder.UserId);
                        //cmd.Parameters.AddWithValue("@OrderDate", updatedOrder.OrderDate);
                        cmd.Parameters.AddWithValue("@TotalPrice", updatedOrder.TotalPrice);
                        cmd.Parameters.AddWithValue("@OrderStatus", updatedOrder.OrderStatus);
                        cmd.Parameters.AddWithValue("@EmployeeId", updatedOrder.EmployeeId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@OrderId", orderId);

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
        [Route("delete-order/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    // Önce siparişle ilgili diğer tablolardaki bağıntıları silebilirsiniz.
                    var deleteDetailsQuery = "DELETE FROM order_details WHERE OrderID = @OrderId";
                    using (var deleteDetailsCmd = new MySqlCommand(deleteDetailsQuery, conn))
                    {
                        deleteDetailsCmd.Parameters.AddWithValue("@OrderId", orderId);
                        await deleteDetailsCmd.ExecuteNonQueryAsync();
                    }

                    // Sonra ana siparişi silebilirsiniz.
                    string query = "DELETE FROM orders WHERE OrderID = @OrderId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);

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