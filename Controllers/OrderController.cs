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
        public OrderController ()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-order")]
        public async Task<IActionResult> GetOrder()
        {
            try 
            {
                using(var conn= new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM orders";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using(var reader = await cmd.ExecuteReaderAsync())
                        {
                            var orders = new List<Order>();
                            while ( await reader.ReadAsync() )
                            {
                                var order = new Order
                                {
                                    OrderId = Convert.ToInt32(reader["OrderID"]),
                                    UserId = Convert.ToInt32(reader["UserID"]),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                    TotalPrice =Convert.ToDecimal(reader["TotalPrice"]),
                                    OrderStatus =Convert.ToInt32(reader["OrderStatus"]),
                                    EmplooyeeId =Convert.ToInt32(reader["EmployeID"]),

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
                return StatusCode ( 500, ex.Message );
            }
        }

    }
}