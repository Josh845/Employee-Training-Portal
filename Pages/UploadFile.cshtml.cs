using Employee_Training_Portal.Data;
using Employee_Training_Portal.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Employee_Training_Portal.Pages
{
    public class UploadFileModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string Message { get; set; }  

        [BindProperty]
        public List<UploadFile> Files { get; set; }

        [BindProperty]
        public List<Video> videoURL { get; set; }

        public UploadFileModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            //retrieve database files as a list
            Files = _db.UploadFile.ToList();
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <param name="videoAction"></param>
        /// <returns>List of video URL links 
        /// as well as providing 
        /// </returns>
        /// <exception cref="Exception"></exception>
        public async Task<IActionResult> OnPostUploadVideo(int id, string url, string videoAction)
        {
            try
            {
                //btn selected to enter url 
                if (videoAction == "uploadVideo" && url != null)
                {
                    
                    //create video model to insert into db 
                    var URL = new Video
                    {
                        videoURL = url
                    };
                    //insert url link into the video table 
                    _db.VideoFile.Add(URL);
                    await _db.SaveChangesAsync();
                    return Page();
                }
                else if(videoAction == "delete") 
                {
                    var deleteVideo =  _db.VideoFile.Find(id); // get id of the specified video URL to delete
                    if (deleteVideo != null)
                    {
                        _db.VideoFile.Remove(deleteVideo); //remove videoURL from db
                        await _db.SaveChangesAsync();
                    }
                }
                else 
                {
                    videoURL = _db.VideoFile.ToList();
                    return Page();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Invalid url provided" + ex.Message);
            }
           
            return Page(); 
        }

        /// <summary>
        /// On Post method the files are populated into the 
        /// database and given their assigned ID for retrieval
        /// Using EF CORE 
        /// </summary>
        /// <returns>Message that the files have been uploaded</returns>
        public async Task<IActionResult> OnPostAsync(int id, IFormFile file, string action)
        {
            try
            {

                if (Files.Count > 10)//check to only allow 10 files
                {
                    ModelState.AddModelError("", "Only Allowed a Maximum of 10 files");
                    return Page();
                }
                else
                {
                  
                    if (action == "upload" && file != null)
                    {
                        // Save the uploaded file to the database
                        var newFile = new UploadFile
                        {
                            fileName = file.FileName,
                            contentType = file.ContentType,
                            Data = new byte[file.Length]
                        };
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);
                            newFile.Data = stream.ToArray();
                        }
                        _db.UploadFile.Add(newFile);
                        await _db.SaveChangesAsync();
                    }
                    else if (action == "delete")
                    {
                        // Delete the specified file from the database
                        var fileToDelete = await _db.UploadFile.FindAsync(id);
                        if (fileToDelete != null)
                        {
                            _db.UploadFile.Remove(fileToDelete);
                            await _db.SaveChangesAsync();
                        }
                    }

                    // Refresh the list of files and return to the page
                    Files = _db.UploadFile.ToList();
                    return Page();
                }
            }
            catch(FileLoadException FileError) 
            {
                throw new FileLoadException("Invalid file uploaded please try again\n" + FileError.Message);

            }
            finally
            {
                Message = "Success";
            }
        }
    }
}