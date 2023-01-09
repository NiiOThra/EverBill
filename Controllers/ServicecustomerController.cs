using EverBill.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EverBill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicecustomerController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ServicecustomerController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query =
            @"SELECT CustomerName, Customer.CustomerId, Services.CustomerId, ServiceName, ServicePrice, ServiceQty
            FROM Customer
            INNER JOIN Services
            ON Customer.CustomerId = Services.CustomerId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EverBillAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query =
            @"SELECT CustomerName, Customer.CustomerId, Services.CustomerId, ServiceName, ServicePrice, ServiceQty, ServiceTotal, ServiceId
            FROM Customer
            INNER JOIN Services
            ON Customer.CustomerId = Services.CustomerId
            WHERE Customer.CustomerId = @CustomerId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EverBillAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CustomerId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

    }
}
