using System.ComponentModel.DataAnnotations;

namespace Soccer.Models;

public class Team
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле є обов'язковим для заповнення")]
    [Display(Name = "Назва клубу")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Поле є обов'язковим для заповнення")]
    [Display(Name = "Головний тренер")]
    public required string Coach { get; set; }

    public ICollection<Player> Players { get; set; } = new List<Player>();
}