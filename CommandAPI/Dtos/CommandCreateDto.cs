namespace CommandAPI.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class CommandCreateDto
    {
        [MaxLength(250)]
        [Required]
        public string HowTo { get; set; }
      
        [Required]
        public string Line { get; set; }

        [Required]
        public string  Platform { get; set; }
    }
}