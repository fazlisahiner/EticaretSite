using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//using MySql.Data.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using EticaretSite.Models;

namespace EticaretSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly string _connectionString;

        public AuthController()
        {
            _connectionString = "server=localhost; database=mydatabase; user=myuser; password=mypassword";
        }



    }
}
