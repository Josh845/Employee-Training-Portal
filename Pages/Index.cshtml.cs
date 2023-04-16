﻿using Employee_Training_Portal.Data;
using Employee_Training_Portal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            [BindProperty]
            public string pass_word { get; set; } //get user password from html form
        
        public void OnGet()
        {
            
        }

        //function used to route to employee view
        public async Task<IActionResult> OnPost() {
            //basic authentication 
                    try
                    {
              
                _db.Database.OpenConnection();
                var userEmployeeName = _db.Employee.FirstOrDefault().fname.ToString().TrimEnd(); //gets employee name from db and trims
                var userEmployerName = _db.Employer.FirstOrDefault().fname.ToString().TrimEnd(); //gets employer name from db and trims

                var userEmployee_pass = _db.Employee.FirstOrDefault().password.ToString().TrimEnd(); //
                var userEmployer_pass = _db.Employer.FirstOrDefault().password.ToString().TrimEnd();

                if (userEmployerName == user_name && userEmployer_pass  == pass_word)
                        {
                            Console.WriteLine("Authenticated Employer");
                            return RedirectToPage("Employer");
                        }
                        else if (userEmployeeName == user_name && userEmployee_pass == pass_word)
                        {
                            Console.WriteLine("Authenticated Employee");
                            return RedirectToPage("Employee");
                       }
                
                        else
                        {
                            Console.WriteLine("Authentication Failed");
                            return RedirectToPage("Index");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return RedirectToPage("Error");
                    }

                    _db.Database.CloseConnection();
                    return RedirectToPage("Index");
        }

    }
}
