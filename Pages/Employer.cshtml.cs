using Employee_Training_Portal.Data;
using Employee_Training_Portal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;

namespace Employee_Training_Portal.Pages
{

    public class EmployerModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        public EmployerModel(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        Employer employer { get; set; } //employer model

        Employee employee { get; set; } //employee model

        Progress progress { get; set; } //progress model 

        [ViewData]
        int employeeScore { get; set; } //display employee score 

        [ViewData]
        string viewDeadLine { get; } //display deadline message


        [BindProperty]
        DateTime set_deadline { get; set; } = DateTime.Today;  //get deadline set from the calendar


        public void OnGet()
        {

        }

        public void OnPost()
        { //method to work with submitting the actions performed from employer


        }
        /// <summary>
        /// Retrieves the score from the employee 
        /// 
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void OnPostViewScore()
        { //method to get the score for the Employee 

            try
            {
                //query to get the employee score
                //set property to retrieve score
                var employeeScore = _db.Progress.First().score;

                if (employeeScore == 100)
                {
                    ViewData["employeeScore"] = "Employee Has Completed The Training: " + employeeScore + "/100%";
                }
                else
                {

                    ViewData["employeeScore"] = "Employee Progress: " + employeeScore + "/100%";
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message + " \n Employee has not completed the training");
            }
            finally
            {
                RedirectToPage("Employer");
            }

        }

        /// <summary>
        /// After Employer selects the date in the calendar provided 
        /// The Function will take the input from the HTML calendar 
        /// Pass Value to the query to set the deadline 
        /// Changes are reflected in the progress table 
        /// Deadline date will be set for the Employee
        /// </summary>
        public void OnPostSetDeadLine()
        {

            try
            {
                //check database to ensure the deadline has not been set
                var deadLineIsSet = _db.Progress.FirstOrDefault().deadline;


                if (deadLineIsSet >= DateTime.Today.AddDays(15))
                {
                    ViewData["viewDeadLine"] = "Deadline has already been set to : " + deadLineIsSet;
                }
                else
                {
                    //add 15 days to the deadline and save the changes
                    set_deadline = DateTime.Today.AddDays(15);
                    _db.Progress.FirstOrDefault().deadline = set_deadline;
                    _db.SaveChanges();
                    ViewData["viewDeadLine"] = "Deadline has been set to: " + set_deadline;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong Please contact your IT administrator :( " + "\n Exception: " + ex.Message);
            }
            finally
            {
                RedirectToPage("Index");
            }
        }

        /// <summary>
        /// Intended to reset the employee's score and employee's deadline 
        /// Based on the current day 
        /// </summary>
        public void OnPostReset()
        {

            try
            {
                var deadLineIsSet = _db.Progress.FirstOrDefault().deadline;

                if (deadLineIsSet >= DateTime.Today)
                {
                    //set deadline back to minimum  
                    set_deadline = DateTime.MinValue;
                    _db.Progress.FirstOrDefault().deadline = set_deadline;
                    _db.SaveChanges();
                    ViewData["viewDeadLine"] = "Deadline Reset!";
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong Please contact your IT administrator :( " + "\n Exception: " + ex.Message);
            }
            finally
            {
                RedirectToPage("Index");
            }

        }

    }
}