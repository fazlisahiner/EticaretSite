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
        public OrderDetailController ()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-orderdetail")]
        public async Task<IActionResult> GetOrderDetail()
        {
            try 
            {
                using(var conn= new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM order_details";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using(var reader = await cmd.ExecuteReaderAsync())
                        {
                            var orderDetails = new List<OrderDetail>();
                            while ( await reader.ReadAsync() )
                            {
                                var orderDetail = new OrderDetail
                                {
                                    DetailId = Convert.ToInt32(reader["DetailID"]),
                                    OrderId = Convert.ToInt32(reader["OrderID"]),
                                    ProductId = Convert.ToInt32(reader["ProductID"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    ProductPrice =Convert.ToDecimal(reader["PoductPrice"]),
                                    Discount =Convert.ToDecimal(reader["Discount"]),
                                    LineTotal =Convert.ToDecimal(reader["LineTotal"]),

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
                return StatusCode ( 500, ex.Message );
            }
        }

    }
}