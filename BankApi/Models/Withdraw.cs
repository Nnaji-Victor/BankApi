using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class Withdraw
    {
        public string UserId { get; set; }
        public string Date { get; set; }
        public double WithdrawAmount { get; set; }
    }
}
