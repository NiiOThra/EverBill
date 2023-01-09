using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EverBillBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceSumController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ServiceSumController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query =
            @"SELECT (SELECT SUM(ServiceTotal) 
            FROM Services 
            WHERE CustomerId = @CustomerId) 
            AS Total";

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
