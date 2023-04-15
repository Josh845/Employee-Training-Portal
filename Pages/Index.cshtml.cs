using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Training_Portal.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string userpass { get; set; }    //get username 

        [BindProperty]
        public string password { get; set; } //get user password
        public void OnGet()
        {
                    //method for first loading into page

        }

        //function used to route to employee view
        public async Task<IActionResult> OnPost(string user, string pass) {
            //basic authentication
            try
            {              
                if (user == "j" && pass == "pass")
                {
                    Console.WriteLine("Authenticated Employer");
                    return RedirectToPage("Employer");
                }
                else if (user == "employee1" && pass == "test")
                {
                    Console.WriteLine("Authenticated Employee");
                    return RedirectToPage("Employee");
                }
                else { Console.WriteLine("Authentication Failed"); }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToPage("Error");
            }

            return RedirectToPage("Employee");
        }

    }
}
