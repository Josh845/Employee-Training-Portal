using Employee_Training_Portal.Data;
using Employee_Training_Portal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        [ViewData]
        public DateTime? deadline { get; set; } //display date 

        private readonly ApplicationDbContext _db;

        public EmployeeModel(ApplicationDbContext db)
        {

            _db = db;

        }
        public List<string> videoUrls { get; set; }

        public List<UploadFile> textFiles { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var _videoUrls = await _db.VideoFile.Select(i => i.videoURL).ToListAsync();
            videoUrls = _videoUrls;

            textFiles = await _db.UploadFile.ToListAsync();

            employee = _db.Employee.FirstOrDefault();
            deadline = _db.Progress.FirstOrDefault().deadline; // set deadline 

            return Page();
        }

        public void OnPost()
        {
            try
            {
                _db.Progress.FirstOrDefault().score = 100;
                ViewData["success"] = "Training Completed! ";

            }
            catch (Exception e)
            {
                ViewData["success"] = e.Message;
                ViewData["error"] = e.StackTrace;

            }
            finally
            {
                _db.SaveChanges();
            }
        }
    }
}
