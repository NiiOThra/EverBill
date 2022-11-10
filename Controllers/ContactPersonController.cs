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
    public class ContactPersonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ContactPersonController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ContactPersonId, ContactFullName, ContactPhoneNumber, ContactCompany
                            from
                            dbo.ContactPerson
                            ";

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
        public JsonResult Post(ContactPerson conp)
        {
            string query = @"
                           insert into dbo.ContactPerson
                           (ContactFullName, ContactPhoneNumber, ContactCompany)
                           values (@ContactFullName, @ContactPhoneNumber, @ContactCompany)
                           ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EverBillAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ContactFullName", conp.ContactFullName);
                    myCommand.Parameters.AddWithValue("@ContactPhoneNumber", conp.ContactPhoneNumber);
                    myCommand.Parameters.AddWithValue("@ContactCompany", conp.ContactCompany);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        
        [HttpPut]
        public JsonResult Put(ContactPerson conp)
        {
            string query = @"
                           update dbo.ContactPerson
                           set ContactFullName = @ContactFullName,
                           ContactPhoneNumber = @ContactPhoneNumber,
                           ContactCompany = @ContactCompany
                           where ContactPersonId = @ContactPersonId
                           ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EverBillAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ContactPersonId", conp.ContactPersonId);
                    myCommand.Parameters.AddWithValue("@ContactFullName", conp.ContactFullName);
                    myCommand.Parameters.AddWithValue("@ContactPhoneNumber", conp.ContactPhoneNumber);
                    myCommand.Parameters.AddWithValue("@ContactCompany", conp.ContactCompany);
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
                           delete from dbo.ContactPerson
                           where ContactPersonId = @ContactPersonId
                           ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EverBillAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ContactPersonId", id);

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

