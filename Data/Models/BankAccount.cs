using System;
using System.ComponentModel.DataAnnotations;

namespace microbank.Data.Models
{
    public class BankAccount
    {
        public Guid Id {get; set;}

        [Required, StringLength(7)]
        public string AccountNumber {get; set;}
    }
}