using System.ComponentModel.DataAnnotations;

namespace Misaar.Models
{
    public class Picture 
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string PictureName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public PictureType PictureType { get; set; }
        public int? ArticleId { get; set; }
        public virtual Article Article { get; set; }
        
    }

    public enum PictureType
    {
        Main = 1,
        Secondary,
        Third
    }
}