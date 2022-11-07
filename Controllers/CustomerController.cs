using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using EverBill.Models;
using System.IO;

namespace EverBill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public CustomerController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select CustomerId, CustomerName, CustomerCVRnumber, CustomerPhoneNumber, CustomerAddress, CustomerEmail, ListOfProjects
                            from
                            dbo.Customer
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
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
        public JsonResult Post(Customer cus)
        {
            string query = @"
                           insert into dbo.Customer
                           (CustomerName, CustomerCVRnumber, CustomerPhoneNumber, CustomerAddress, CustomerEmail, ListOfProjects)
                    values (@CustomerName, @CustomerCVRnumber, @CustomerPhoneNumber, @CustomerAddress, @CustomerEmail, @ListOfProjects)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CustomerName", cus.CustomerName);
                    myCommand.Parameters.AddWithValue("@CustomerCVRnumber", cus.CustomerCVRnumber);
                    myCommand.Parameters.AddWithValue("@CustomerPhoneNumber", cus.CustomerPhoneNumber);
                    myCommand.Parameters.AddWithValue("@CustomerAddress", cus.CustomerAddress);
                    myCommand.Parameters.AddWithValue("@CustomerEmail", cus.CustomerEmail);
                    myCommand.Parameters.AddWithValue("@ListOfProjects", cus.ListOfProjects);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        
        [HttpPut]
        public JsonResult Put(Customer cus)
        {
            string query = @"
                           update dbo.Customer
                           set CustomerName = @CustomerName,
                            CustomerCVRnumber = @CustomerCVRnumber,
                            CustomerPhoneNumber = @CustomerPhoneNumber,
                            CustomerAddress = @CustomerAddress,
                            CustomerEmail = @CustomerEmail,
                            ListOfProjects = @ListOfProjects
                            where CustomerId = @CustomerId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CustomerId", cus.CustomerId);
                    myCommand.Parameters.AddWithValue("@CustomerName", cus.CustomerName);
                    myCommand.Parameters.AddWithValue("@CustomerCVRnumber", cus.CustomerCVRnumber);
                    myCommand.Parameters.AddWithValue("@CustomerPhoneNumber", cus.CustomerPhoneNumber);
                    myCommand.Parameters.AddWithValue("@CustomerAddress", cus.CustomerAddress);
                    myCommand.Parameters.AddWithValue("@CustomerEmail", cus.CustomerEmail);
                    myCommand.Parameters.AddWithValue("@ListOfProjects", cus.ListOfProjects);
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
                           delete from dbo.Customer
                            where CustomerId=@CustomerId
                            ";
            
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
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

            return new JsonResult("Deleted Successfully");
        }


    }
}

