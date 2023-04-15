using System.ComponentModel.DataAnnotations;

namespace Employee_Training_Portal.Model
{
    public class Employee
    {
        //create all properties for Employee model
        [Key]
        public int employeeID { get; set; } = 1;
        public string fname { get; set; }

        public string lname { get; set; }

        public string password { get; set; }

        public int score { get; set; }
    }
}
