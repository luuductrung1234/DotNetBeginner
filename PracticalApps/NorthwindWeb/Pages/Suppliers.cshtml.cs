using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PracticalApps.NorthwindContextLib;
using PracticalApps.NorthwindEntitiesLib;

namespace PracticalApps.NorthwindWeb.Pages
{
    public class SuppliersModel : PageModel
    {
        private NorthwindContext db;

        public SuppliersModel(NorthwindContext injectedContext)
        {
            db = injectedContext;
        }

        public IEnumerable<string> Suppliers { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind Web Site - Suppliers";

            Suppliers = db.Suppliers.Select(s => s.CompanyName);
        }

        [BindProperty]
        public Supplier Supplier { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(Supplier);
                db.SaveChanges();
                return RedirectToPage("/suppliers");
            }
            return Page();
        }
    }
}