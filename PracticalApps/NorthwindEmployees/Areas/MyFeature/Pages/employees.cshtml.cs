using Microsoft.AspNetCore.Mvc.RazorPages; // PageModel
using System.Linq;                         // ToArray()
using System.Collections.Generic;          // IEnumerable<T>
using PracticalApps.NorthwindContextLib;
using PracticalApps.NorthwindEntitiesLib;

namespace PracticalApps.MyFeatures.Pages
{
    public class EmployeesPageModel : PageModel
    {
        private NorthwindContext db;

        public EmployeesPageModel(NorthwindContext injectedContext)
        {
            db = injectedContext;
        }

        public IEnumerable<Employee> Employees { get; set; }

        public void OnGet()
        {
            Employees = db.Employees.ToArray();
        }
    }
}