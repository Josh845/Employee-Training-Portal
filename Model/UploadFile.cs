using System.ComponentModel.DataAnnotations;

namespace Employee_Training_Portal.Model
{
    public class UploadFile
    {
        [Key]
        [Required]
        public int fileID   { get; set; }
        public string fileName { get; set; }
        public string contentType { get; set; }
        public byte[] Data {get; set; }

    }
}
