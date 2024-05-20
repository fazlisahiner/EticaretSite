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
            _connectionString= "server=localhost; database=eticaretsite; user=root; password=";
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
                    string query = "SELECT*FROM category";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using ( var reader = await cmd.ExecuteReaderAsync())
                        {
                            var categories = new List<Category>();
                            while(await reader.ReadAsync() )
                            {
                                var category = new Category
                                {
                                    CategoryId = Convert.ToInt32(reader["CategoryID"]),
                                    CategoryName = reader["CategoryName"].ToString(),

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
    }
}