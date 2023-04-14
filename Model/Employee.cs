using System.ComponentModel.DataAnnotations;

namespace Employee_Training_Portal.Model
{
    public class Employee
    {
        //create all properties for Employee model
        [Key]
        [Required]
        public int employeeID { get; set; }
        public string fname { get; set; } = "employee";

        public string lname { get; set; }

        public string password { get; set; } = "employee1"; 

        public int score { get; set; }
    }
}
