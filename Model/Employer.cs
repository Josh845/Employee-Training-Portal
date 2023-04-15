using System.ComponentModel.DataAnnotations;

namespace Employee_Training_Portal.Model
{
    public class Employer
    {
        [Key]
        [Required]
        public int employerID { get; set; } = 1; 
        public string fname { get; set; } = "junaid";

        public string lname { get; set; } = "zubairi";

        public string password { get; set; } = "zub1"; 

    }
}
