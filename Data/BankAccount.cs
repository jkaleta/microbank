using System;
using System.ComponentModel.DataAnnotations;

namespace microbank.Data
{
    public class BankAccount
    {
        public Guid Id {get; set;}

        [Required, StringLength(7)]
        public string AccountNumber {get; set;}
    }
}