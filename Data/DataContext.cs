using Microsoft.EntityFrameworkCore;

namespace microbank.Data
{
    public class MicroBankContext : DbContext
    {
        public MicroBankContext(DbContextOptions options)
            : base(options)
        {
        }

        // public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Models.Customer> Customers {get; set;}
    }
}