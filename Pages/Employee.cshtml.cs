using Employee_Training_Portal.Data;
using Employee_Training_Portal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Employee_Training_Portal.Pages
{
    public class EmployeeModel : PageModel
    {

        public Employee employee { get; set; }
        public Progress progress { get; set; }

        [ViewData]
        public int get_score { get; } //display progress bar 

        [ViewData]
        public string success { get; set; } //display submission response     

        private readonly ApplicationDbContext _db;
        public EmployeeModel(ApplicationDbContext db){

            _db = db;

        }

        public void OnGet()
        {
            employee = _db.Employee.FirstOrDefault();

        }

        public void OnPost()
        {
            try
            {
                _db.Progress.FirstOrDefault().score = 100;
                ViewData["success"] = "Training Completed! ";
               
            }
            catch(Exception e)
            {
                ViewData["success"] = e.Message;
                ViewData["error"] = e.StackTrace;
                
            }
            finally
            {
                _db.SaveChanges();               
            }
        }




        /*
        public IActionResult OnPostSaveScore(int progressBarValue)
        {
            get the employee
            var employee = _db.Employee.FirstOrDefault();
            
           
                _db.Progress.FirstOrDefault().score = progressBarValue;
                ViewData["success"] = "Progress has been saved! ";
                _db.SaveChanges();
            
            return Page();
        }*/
    }
}
