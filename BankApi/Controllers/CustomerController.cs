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
    public class CustomerController : ControllerBase
    {
        SqlConnection con = new SqlConnection(LoginController.conString);
        // GET: api/Customer
        [HttpGet("balance")]
        public string Get()
        {
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
        [HttpPost("deposit")]
        public Response Post(Deposit deposit)
        {
            string accNum = "", balance = "";


            con.Open();
            string query = "select accountId,balance from [Account] where customerId=@user";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user", deposit.UserId);

            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            

            if (dr.Read())
            {
                accNum = dr[0].ToString();
                balance = dr[1].ToString();
            }
            con.Close();

            con.Open();
            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            transaction = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = transaction;


            try
            {
                command.Parameters.AddWithValue("@accNum", accNum);
                command.Parameters.AddWithValue("@date", deposit.Date);
                command.Parameters.AddWithValue("@bal", balance);
                command.Parameters.AddWithValue("@deposit", deposit.DepositAmount);

                command.CommandText = "INSERT INTO Transactions(AccountId,date,balance,deposit) VALUES(@accNum,@date,@bal,@deposit)";
                command.ExecuteNonQuery();

                command.CommandText = "update [Account] set balance = balance +@deposit where accountId=@accNum";
                command.ExecuteNonQuery();

                transaction.Commit();

                return new Response
                { Status = "Success", Message = "Cash sucessfully deposited" };

            }
            catch
            {
                transaction.Rollback();
                return new Response
                { Status = "Failed", Message = "Deposit failed!." };
            }
            finally
            {
                con.Close();
            }
        }

        // POST: api/Customer
        [HttpPost("withdraw")]
        public Response Post(Withdraw withdraw)
        {
            string accNum = "", balance = "";


            con.Open();
            string query = "select accountId,balance from [Account] where customerId=@user";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user", withdraw.UserId);

            SqlDataReader dr;
            dr = cmd.ExecuteReader();



            if (dr.Read())
            {
                accNum = dr[0].ToString();
                balance = dr[1].ToString();
            }
            con.Close();

            con.Open();
            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            transaction = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = transaction;


            try
            {
                command.Parameters.AddWithValue("@accNum", accNum);
                command.Parameters.AddWithValue("@date", withdraw.Date);
                command.Parameters.AddWithValue("@bal", balance);
                command.Parameters.AddWithValue("@withdraw", withdraw.WithdrawAmount);

                command.CommandText = "INSERT INTO Transactions(AccountId,date,balance,deposit) VALUES(@accNum,@date,@bal,@withdraw)";
                command.ExecuteNonQuery();

                command.CommandText = "update [Account] set balance = balance +@deposit where accountId=@accNum";
                command.ExecuteNonQuery();

                transaction.Commit();

                return new Response
                { Status = "Success", Message = "Cash sucessfully deposited" };

            }
            catch
            {
                transaction.Rollback();
                return new Response
                { Status = "Failed", Message = "Deposit failed!." };
            }
            finally
            {
                con.Close();
            }
        }

    }
}
