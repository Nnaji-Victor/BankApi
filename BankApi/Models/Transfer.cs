using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class Transfer
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }

    }
}
