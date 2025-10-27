using System.ComponentModel.DataAnnotations;
namespace Soccer.Models
{
    public class Players
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field must be set")]
        [Display(Name = "Player name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The field must be set")]
        [Display(Name = "Player's year of birth")]
        public int BirthYear { get; set; }
        [Required(ErrorMessage = "The field must be set")]
        [Display(Name = "Player position")]
        public string? Position { get; set; }
        [Required(ErrorMessage = "The field must be set")]
        public int TeamId { get; set; }
        public Teams? Team { get; set; }
    }
}
