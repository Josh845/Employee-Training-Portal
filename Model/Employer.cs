using System.ComponentModel.DataAnnotations;

namespace Employee_Training_Portal.Model
{
    public class Employer
    {
        [Key]
        [Required]
        public int employerID { get; set; }
        public string fname { get; set; } = "zubairi";

        public string lname { get; set; }

        public string password { get; set; } = "zub1"; 

    }
}
