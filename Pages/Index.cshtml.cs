using Employee_Training_Portal.Data;
using Employee_Training_Portal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Employee_Training_Portal.Pages
{
    public class IndexModel : PageModel
    {

        //  private readonly ILogger<IndexModel> _logger;
        /* public IndexModel(ILogger<IndexModel> logger)
         {
             _logger = logger;
         }
         */
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        

        [BindProperty]
        public string user_name { get; set; }    //get username from html form

        [BindProperty, DataType(DataType.Password)]
        public string pass_word { get; set; } //get user password from html form

        [ViewData]
        public string inc_pass { get; } //displays that incorrect credentials were provided

        public void OnGet()
        {


        }

        //function used to route to employee view

        public async Task<IActionResult> OnPost()
        {

            //basic authentication 
            try
            {
                //  var user = configuration.GetSection("SiteUser").Get<SiteUser>();

                var userEmployeeName = _db.Employee.FirstOrDefault().fname.ToString().TrimEnd(); //gets employee name from db and trims
                var userEmployerName = _db.Employer.FirstOrDefault().fname.ToString().TrimEnd(); //gets employer name from db and trims

                var userEmployee_pass = _db.Employee.FirstOrDefault().password.ToString().TrimEnd(); //gets employee name from db and trims whitespace
                var userEmployer_pass = _db.Employer.FirstOrDefault().password.ToString().TrimEnd();//gets employer name from db and trims whitespace


                //authentication
                if (user_name == userEmployerName && pass_word == userEmployer_pass)
                {
                    return RedirectToPage("/Employer");
                }

                else if (user_name == userEmployeeName && pass_word == userEmployee_pass)
                {
                    Console.WriteLine("Authenticated Employee");
                    return RedirectToPage("/Employee");
                }
                else
                {
                    ViewData["inc_pass"] = "Incorrect Username or Password";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToPage("Error");
            }
        }
    }
}