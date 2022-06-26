
namespace CommandAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class  RegisterUserDto
    {
        [Required]
        [EmailAddress]
        public string  Email { get; set; }     

        [Required]
        public string Password { get; set; }
    }
}