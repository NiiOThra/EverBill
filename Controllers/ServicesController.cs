﻿using EverBill.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EverBill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ServicesController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        

        [HttpGet]
        public JsonResult Get()
        {
            string query =
            @"select 
            ServiceId, ServiceName, ServicePrice, ServiceQty, ServiceTotal, CustomerId
            from dbo.Services";

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
        public JsonResult Post(Services serv)
        {
            string query =
            @"insert into dbo.Services
            (ServiceName, ServicePrice, ServiceQty, ServiceTotal, CustomerId)
            values 
            (@ServiceName, @ServicePrice, @ServiceQty, @ServiceTotal, @CustomerId)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EverBillAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ServiceName", serv.ServiceName);
                    myCommand.Parameters.AddWithValue("@ServicePrice", serv.ServicePrice);
                    myCommand.Parameters.AddWithValue("@ServiceQty", serv.ServiceQty);
                    myCommand.Parameters.AddWithValue("@ServiceTotal", serv.ServiceTotal);
                    myCommand.Parameters.AddWithValue("@CustomerId", serv.CustomerId);
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
        public JsonResult Put(Services serv)
        {
            string query =
            @"update dbo.Services set 
            ServiceName = @ServiceName, 
            ServicePrice = @ServicePrice,
            ServiceQty = @ServiceQty,
            ServiceTotal = @ServiceTotal,
            CustomerId = @CustomerId
            where ServiceId = @ServiceId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EverBillAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ServiceId", serv.ServiceId);
                    myCommand.Parameters.AddWithValue("@ServiceName", serv.ServiceName);
                    myCommand.Parameters.AddWithValue("@ServicePrice", serv.ServicePrice);
                    myCommand.Parameters.AddWithValue("@ServiceQty", serv.ServiceQty);
                    myCommand.Parameters.AddWithValue("@ServiceTotal", serv.ServiceTotal);
                    myCommand.Parameters.AddWithValue("@CustomerId", serv.CustomerId);
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
                           delete from dbo.Services
                           where ServiceId = @ServiceId
                           ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EverBillAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ServiceId", id);

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
