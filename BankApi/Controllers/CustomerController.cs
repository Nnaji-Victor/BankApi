using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BankApi.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        // GET: api/Customer
        [HttpGet("balance")]
        public string Get()
        {
            SqlConnection con = new SqlConnection(LoginController.conString);
            string customer = LoginController.UserID;
            con.Open();
            string query = "select accountId,balance from [Account] where customerId=@user";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user", customer);

            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string acc = dr[1].ToString();
                con.Close();
                return acc;
            }
            else
            {
                con.Close();
                return "invalid";
            }

            
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
