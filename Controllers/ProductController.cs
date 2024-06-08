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

        public ProductController()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
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
                    using (var cmd = new MySqlCommand(query, conn))
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
                                    CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                                    UpdateDate = Convert.ToDateTime(reader["UpdateDate"]),
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
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }

        }

        [HttpGet]
        [Route("get-product-by-category/{CategoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int CategoryId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM product Where CategoryID =@CategoryId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
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
                                    CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                                    UpdateDate = Convert.ToDateTime(reader["UpdateDate"]),
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
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }

        }

        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "INSERT INTO product (ProductCode, ProductName, Price, StockQuantity, CreateDate, UpdateDate, Description, CategoryID, Brand, ImageUrl) VALUES (@ProductCode, @ProductName, @Price, @StockQuantity, @CreateDate, @UpdateDate, @Description, @CategoryID, @Brand, @ImageUrl)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductCode", product.ProductCode);
                        cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                        cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.Parameters.AddWithValue("@CategoryID", product.CategoryId);
                        cmd.Parameters.AddWithValue("@Brand", product.Brand);
                        cmd.Parameters.AddWithValue("@ImageUrl", product.ImageUrl);
                        await cmd.ExecuteNonQueryAsync();
                        return Ok();
                    }
                }
            }
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }

        [HttpPut]
        [Route("update-product/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] Product updatedProduct)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE product SET ProductName = @ProductName, Price = @Price, StockQuantity = @StockQuantity, Description = @Description, ImageUrl = @ImageUrl, UpdateDate = @UpdateDate WHERE ProductID = @ProductID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", updatedProduct.ProductName);
                        cmd.Parameters.AddWithValue("@Price", updatedProduct.Price);
                        cmd.Parameters.AddWithValue("@StockQuantity", updatedProduct.StockQuantity);
                        cmd.Parameters.AddWithValue("@Description", updatedProduct.Description);
                        cmd.Parameters.AddWithValue("@ImageUrl", updatedProduct.ImageUrl);
                        cmd.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
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
        [Route("get-product-details/{productId}")]
        public async Task<IActionResult> GetProductDetails(int productId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM product WHERE ProductID = @ProductID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var product = new Product
                                {
                                    ProductId = Convert.ToInt32(reader["ProductID"]),
                                    ProductCode = reader["ProductCode"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                    CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                                    UpdateDate = Convert.ToDateTime(reader["UpdateDate"]),
                                    Description = reader["Description"].ToString(),
                                    CategoryId = Convert.ToInt32(reader["CategoryID"]),
                                    Brand = reader["Brand"] == DBNull.Value ? null : reader["Brand"].ToString(),
                                    ImageUrl = reader["ImageUrl"].ToString()
                                };
                                return Ok(product);
                            }
                            else
                            {
                                return NotFound();
                            }
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
        [Route("search-products")]
        public async Task<IActionResult> SearchProducts(string searchTerm)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM product WHERE ProductName LIKE @SearchTerm";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
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
                                    CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                                    UpdateDate = Convert.ToDateTime(reader["UpdateDate"]),
                                    Description = reader["Description"].ToString(),
                                    CategoryId = Convert.ToInt32(reader["CategoryID"]),
                                    Brand = reader["Brand"] == DBNull.Value ? null : reader["Brand"].ToString(),
                                    ImageUrl = reader["ImageUrl"].ToString()
                                };
                                products.Add(product);
                            }
                            return Ok(products);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }

        [HttpDelete]
        [Route("delete-product/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "DELETE FROM product WHERE ProductID = @ProductID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);
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


        /*
                [HttpPut]
        [Route("update-product/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] Product updatedProduct)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE product SET ProductName = @ProductName, Price = @Price, StockQuantity = @StockQuantity, Description = @Description, ImageUrl = @ImageUrl, UpdateDate = @UpdateDate, Brand = COALESCE(@Brand, Brand), ProductCode = COALESCE(@ProductCode, ProductCode) WHERE ProductID = @ProductID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", updatedProduct.ProductName);
                        cmd.Parameters.AddWithValue("@Price", updatedProduct.Price);
                        cmd.Parameters.AddWithValue("@StockQuantity", updatedProduct.StockQuantity);
                        cmd.Parameters.AddWithValue("@Description", updatedProduct.Description);
                        cmd.Parameters.AddWithValue("@ImageUrl", updatedProduct.ImageUrl);
                        cmd.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Brand", updatedProduct.Brand ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProductCode", updatedProduct.ProductCode ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
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
        */
        /*
                [HttpPut]
                [Route("update-product/{productId}")]
                public async Task<IActionResult> UpdateProduct(int productId, [FromBody] Product updatedProduct)
                {
                    try
                    {
                        using (var conn = new MySqlConnection(_connectionString))
                        {
                            await conn.OpenAsync();
                            string query = "UPDATE product SET ProductName = @ProductName, Price = @Price, StockQuantity = @StockQuantity, Description = @Description, ImageUrl = @ImageUrl, UpdateDate = @UpdateDate, Brand = @Brand, ProductCode = @ProductCode WHERE ProductID = @ProductID";
                            using (var cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@ProductName", updatedProduct.ProductName);
                                cmd.Parameters.AddWithValue("@Price", updatedProduct.Price);
                                cmd.Parameters.AddWithValue("@StockQuantity", updatedProduct.StockQuantity);
                                cmd.Parameters.AddWithValue("@Description", updatedProduct.Description);
                                cmd.Parameters.AddWithValue("@ImageUrl", updatedProduct.ImageUrl);
                                cmd.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@Brand", updatedProduct.Brand);
                                cmd.Parameters.AddWithValue("@ProductCode", updatedProduct.ProductCode);
                                cmd.Parameters.AddWithValue("@ProductID", productId);
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
        */
    }
}
