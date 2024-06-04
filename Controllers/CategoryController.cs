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
    public class CategoryController : ControllerBase
    {
        private readonly string _connectionString;
        public CategoryController()
        {
            _connectionString = "server=localhost; database=eticaretsite; user=root; password=";
        }

        [HttpGet]
        [Route("get-category")]
        public async Task<IActionResult> GetCategory()
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT*FROM category WHERE isActive = 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var categories = new List<Category>();
                            while (await reader.ReadAsync())
                            {
                                var category = new Category
                                {
                                    CategoryId = Convert.ToInt32(reader["CategoryID"]),
                                    CategoryName = reader["CategoryName"].ToString(),
                                    IsActive = Convert.ToBoolean(reader["isActive"])

                                };
                                categories.Add(category);
                            }
                            return Ok(categories);
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
        [Route("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "INSERT INTO category (CategoryName) VALUES (@CategoryName)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
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

        [HttpPut]
        [Route("update-category/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] Category updateCategory)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE category SET CategoryName = @CategoryName, isActive=@IsActive WHERE CategoryID=@CategoryID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", updateCategory.CategoryName);
                        cmd.Parameters.AddWithValue("@IsActive", updateCategory.IsActive);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                        
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

        [HttpGet]
        [Route("search-category")]
        public async Task<IActionResult> SearchCategory(string searchTerm)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM category WHERE CategoryName LIKE @SearchTerm COLLATE utf8mb4_general_ci";
                    using (var cmd = new MySqlCommand(query, conn))       // bu üst kısım (COLLATE utf8mb4_general_ci)büyük/küçük harf olayını duyarsız hale getirmek için
                    {
                        string searchPattern = searchTerm;
                        if (!string.IsNullOrEmpty(searchTerm)) // bu if kısmını yaptıkçünkü boş arama yapıldığında tüm kategoryleri getirmesin diye
                        {
                            searchPattern = "%" + searchTerm + "%";
                        }
                        cmd.Parameters.AddWithValue("@SearchTerm", searchPattern);
                        //cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var categories = new List<Category>();
                            while (await reader.ReadAsync())
                            {
                                var category = new Category
                                {
                                    CategoryId = Convert.ToInt32(reader["CategoryID"]),
                                    CategoryName = reader["CategoryName"].ToString(),
                                    IsActive = Convert.ToBoolean(reader["isActive"])
                                };
                                categories.Add(category);
                            }
                            return Ok(categories);
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
        [Route ("delete-category/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE category SET isActive = 0 WHERE CategoryID = @CategoryID";
                    using ( var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected>0)
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
                return StatusCode (500, ex.Message);
            }
        }
    }
}