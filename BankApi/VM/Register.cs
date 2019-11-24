using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.VM
{
    public class Register
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }

        public string AccounType { get; set; }
        public string Description { get; set; }
        public float Balance { get; set; }
        public string Password { get; set; }
        public string AccountId { get; set; }
    }
}
