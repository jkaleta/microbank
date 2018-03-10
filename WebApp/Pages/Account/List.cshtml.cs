using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using microbank.Data;
using microbank.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace microbank.WebApp.Pages.Account
{
    public class ListModel : PageModel
    {
        private readonly MicroBankContext _db;

        public ListModel(MicroBankContext db)
        {
            _db = db;
        }

        [BindProperty]
        public List<Customer> Customers { 
            get{
                return _db.Customers.ToList();
            } 
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage("/Account/Create");
        }
    }
}