using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string CustomerId { get; set; }
        public string AccounType { get; set; }
        public string Description { get; set; }
        public string Balance { get; set; }
        public string Password { get; set; }
        public string AccountId { get; set; }
        public string Date { get; set; }

    }
}
