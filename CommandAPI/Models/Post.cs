using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }

        [Required]
        public Blog Blog{ get; set; }

    }
}