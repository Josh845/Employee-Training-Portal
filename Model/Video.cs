using System;
using System.ComponentModel.DataAnnotations;

namespace Employee_Training_Portal.Model
{
    public class Video
    {
        [Key]
        [Required]
        public int videoID { get; set; }    

        public string videoURL { get; set; }

    }
}