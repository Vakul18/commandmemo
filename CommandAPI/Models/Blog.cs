using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        
        [Required]
        public string Url { get; set; }

        [Required]
        public List<Post> Posts { get; set; }
    }
}