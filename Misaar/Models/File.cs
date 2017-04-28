using System.ComponentModel.DataAnnotations;

namespace Misaar.Models
{
    public class File 
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }


    }

    public enum FileType
    {
        Avatar = 1, Photo
    }
}