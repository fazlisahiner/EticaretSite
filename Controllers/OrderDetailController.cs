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
    public class OrderDetailController : ControllerBase
    {
        private readonly string _connectionString;
        public OrderDetailController()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-orderdetail")]
        public async Task<IActionResult> GetOrderDetail()
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM order_details";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var orderDetails = new List<OrderDetail>();
                            while (await reader.ReadAsync())
                            {
                                var orderDetail = new OrderDetail
                                {
                                    DetailId = Convert.ToInt32(reader["DetailID"]),
                                    OrderId = Convert.ToInt32(reader["OrderID"]),
                                    ProductId = Convert.ToInt32(reader["ProductID"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    ProductPrice = Convert.ToDecimal(reader["PoductPrice"]),
                                    Discount = Convert.ToDecimal(reader["Discount"]),
                                    LineTotal = Convert.ToDecimal(reader["LineTotal"]),

                                };
                                orderDetails.Add(orderDetail);
                            }
                            return Ok(orderDetails);
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
        [Route("get-orderdetail-by-order/{orderId}")]
        public async Task<IActionResult> GetOrderDetailById(int orderId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM order_details WHERE OrderID = @OrderID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderId);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            /*if (await reader.ReadAsync())
                            {
                                var orderDetail = new OrderDetail
                                {
                                    DetailId = Convert.ToInt32(reader["DetailID"]),
                                    OrderId = Convert.ToInt32(reader["OrderID"]),
                                    ProductId = Convert.ToInt32(reader["ProductID"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    //ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                                    ProductPrice = Convert.ToDecimal(reader["PoductPrice"]),
                                    Discount = Convert.ToDecimal(reader["Discount"]),
                                    LineTotal = Convert.ToDecimal(reader["LineTotal"])
                                };
                                return Ok(orderDetail);
                            }
                            else
                            {
                                return NotFound();
                            }*/

                            var orderDetails = new List<OrderDetail>();
                            while (await reader.ReadAsync())
                            {
                                var orderDetail = new OrderDetail
                                {
                                    DetailId = Convert.ToInt32(reader["DetailID"]),
                                    OrderId = Convert.ToInt32(reader["OrderID"]),
                                    ProductId = Convert.ToInt32(reader["ProductID"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    ProductPrice = Convert.ToDecimal(reader["PoductPrice"]),
                                    Discount = Convert.ToDecimal(reader["Discount"]),
                                    LineTotal = Convert.ToDecimal(reader["LineTotal"]),

                                };
                                orderDetails.Add(orderDetail);
                            }
                            return Ok(orderDetails);
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
        [Route("add-orderdetail")]
        public async Task<IActionResult> AddOrderDetail([FromBody] OrderDetail orderDetail)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "INSERT INTO order_details (OrderID, ProductID, Quantity, ProductPrice, Discount, LineTotal) VALUES (@OrderID, @ProductID, @Quantity, @ProductPrice, @Discount, @LineTotal)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderDetail.OrderId);
                        cmd.Parameters.AddWithValue("@ProductID", orderDetail.ProductId);
                        cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                        cmd.Parameters.AddWithValue("@ProductPrice", orderDetail.ProductPrice);
                        cmd.Parameters.AddWithValue("@Discount", orderDetail.Discount);
                        cmd.Parameters.AddWithValue("@LineTotal", orderDetail.LineTotal);

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

    }
}