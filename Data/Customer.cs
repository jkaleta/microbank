using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace microbank.Data
{
    public class Customer
    {
        public Guid Id {get; set;}

        [Required]
        public string FirstName {get; set;}

        [Required]
        public string LastName {get; set;}

        //public List<BankAccount> Accounts {get; set;}
    }
}