using System;
using System.Collections.Generic;
using System.Data;
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
    public class LoginController : ControllerBase
    {
        public static string conString = "Data Source=DESKTOP-D0IE7T9\\SQLSERVER2017DEV;Initial Catalog=Decabank;Integrated Security=True";
        SqlConnection con = new SqlConnection(conString);

        public static string UserID { get; set; }

        // POST: api/Login
        [HttpPost]
        public Response Post(Login login)
        {
            if(login.Username.StartsWith("ADM", StringComparison.OrdinalIgnoreCase))
            {
                con.Open();
                string query = "Select * from [Admin] where username='" + login.Username + "' and password='" + login.password + "'";

                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dtb = new DataTable();

                sda.Fill(dtb);

                if (dtb.Rows.Count == 1)
                {
                    return new Response { Status = "Success", Message = "Login Successfully into Admin" };
                }
                else
                {
                    con.Close();
                    return new Response { Status = "Invalid", Message = "Invalid User." };
                }
                    
               
            }
            else if(login.Username.StartsWith("VIC", StringComparison.OrdinalIgnoreCase))
            {
                con.Open();
                string query = "Select * from [Account] where customerId='" + login.Username + "'";

                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dtb = new DataTable();

                sda.Fill(dtb);
                if (dtb.Rows.Count == 1)
                {
                    string savedPasswordHash = dtb.Rows[0][6].ToString();
                    bool verify = SecuredPasswordHasher.verify(savedPasswordHash, login.password);

                    if (verify)
                    {
                        UserID = login.Username;
                        return new Response { Status = "Success", Message = "Login Successfully into User" };
                    }
                    else
                    {
                        return new Response { Status = "Invalid", Message = "Invalid User." };
                    }
                }
            }
            con.Close();
            return new Response { Status = "Invalid", Message = "Invalid User." };
        }

    }
}
