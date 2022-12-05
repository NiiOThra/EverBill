using EverBill.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EverBill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ProjectsController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        

        [HttpGet]
        public JsonResult Get()
        {
            string query =
            @"select 
            ProjectId, ProjectName, ProjectPrice
            from dbo.Projects";

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
        
        [HttpPost]
        public JsonResult Post(Projects proj)
        {
            string query =
            @"insert into dbo.Projects
            (ProjectName, ProjectPrice)
            values 
            (@ProjectName, @ProjectPrice)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EverBillAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProjectName", proj.ProjectName);
                    myCommand.Parameters.AddWithValue("@ProjectPrice", proj.ProjectPrice);
                    //myCommand.Parameters.AddWithValue("@ProjectListOfTasks", proj.ProjectListOfTasks);
                    //myCommand.Parameters.AddWithValue("@ProjectCompany", proj.ProjectCompany);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Projects proj)
        {
            string query =
            @"update dbo.Projects set 
            ProjectName = @ProjectName, 
            ProjectPrice = @ProjectPrice
            where ProjectId = @ProjectId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EverBillAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProjectId", proj.ProjectId);
                    myCommand.Parameters.AddWithValue("@ProjectName", proj.ProjectName);
                    myCommand.Parameters.AddWithValue("@ProjectPrice", proj.ProjectPrice);
                    //myCommand.Parameters.AddWithValue("@ProjectListOfTasks", proj.ProjectListOfTasks);
                    //myCommand.Parameters.AddWithValue("@ProjectCompany", proj.ProjectCompany);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
                
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Projects
                           where ProjectId = @ProjectId
                           ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EverBillAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProjectId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }


    }
}
