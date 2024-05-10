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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly string _connectionString;

        public ProductController ( )
        {
            _connectionString = "server=localhost; database=eticaretsite; user=myuser; password=mypassword";
        }

        [HttpGet]
        [Route("get-product")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM product";
                    using (var cmd= new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync()) 
                        {
                          var products = new List<Product>();
                            while (await reader.ReadAsync()) 
                            {
                                var product = new Product
                                {
                                    ProductId = Convert.ToInt32(reader["ProductID"]),
                                    ProductCode = reader["ProductCode"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreateAt"]),
                                    UpdatedAt = Convert.ToDateTime(reader["UpdateAt"]),
                                    Description = reader["Description"].ToString(),
                                    CategoryId = Convert.ToInt32(reader["CategoryID"]),
                                    Brand = reader["Brand"].ToString(),
                                    ImageUrl = reader["ImageUrl"].ToString()

                                };
                                products.Add(product);
                            }
                            return Ok(products);
                        
                        }

                    }

                } 
            }
            catch(Exception error) 
            {
                return StatusCode(500, error.Message);
            }

        }

    }
}
