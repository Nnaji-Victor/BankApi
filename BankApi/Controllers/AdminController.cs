using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Models;
using BankApi.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // GET: api/Admin
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };  
        }

        // GET: api/Admin/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Admin
        [Route("Register")]
        [HttpPost]
        public Response Post(Account account)
        {
            SqlConnection con = new SqlConnection(LoginController.conString);

            if (account.FirstName == "" || account.LastName=="" || account.AccountId==""|| account.CustomerId=="" 
                || account.Balance == "" || account.City=="" || account.Description==""|| account.Password==""
                || account.Phone == "" || account.Date == "")
            {
                return new Response { Status = "Error", Message = "One or more fields is empty" };
            }
            else
            {
                con.Open();
                SqlCommand command = con.CreateCommand();
                SqlTransaction transaction;

                transaction = con.BeginTransaction();
                command.Connection = con;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "INSERT INTO Customer(customerId,lastName,firstName,city,phone,date) VALUES(@user, @lname, @fname, @city,@phone,@date)";
                    command.Parameters.AddWithValue("@user", account.CustomerId);
                    command.Parameters.AddWithValue("@lname", account.LastName);
                    command.Parameters.AddWithValue("@fname", account.FirstName);
                    command.Parameters.AddWithValue("@city", account.City);
                    command.Parameters.AddWithValue("@phone", account.Phone);
                    command.Parameters.AddWithValue("@date", account.Date);
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Account(accountId,customerId,accountType,description,balance, password) " +
                        "VALUES(@accNum,@userr,@accType,@desc,@bal, @password)";
                    command.Parameters.AddWithValue("@accNum", account.AccountId);
                    command.Parameters.AddWithValue("@userr", account.CustomerId);
                    command.Parameters.AddWithValue("@accType", account.AccounType);
                    command.Parameters.AddWithValue("@desc", account.Description);
                    command.Parameters.AddWithValue("@bal", double.Parse(account.Balance));
                    command.Parameters.AddWithValue("@password", SecuredPasswordHasher.hashPassword(account.Password));
                    command.ExecuteNonQuery();



                    transaction.Commit();
                    return new Response { Status = "Success", Message = "New Account Created" };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response { Status = "Error", Message = "An Error has occurred" };
                }
                finally
                {
                    con.Close();
                }
            }
        }

        // PUT: api/Admin/5
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
