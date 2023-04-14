using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Employee_Training_Portal.Model
{
    public class Progress
    {
 
        [Key]
        public int progressId { get; set; }

        [ForeignKey ("employeeID")]
        public int employeeID { get; set; }



    }
}
