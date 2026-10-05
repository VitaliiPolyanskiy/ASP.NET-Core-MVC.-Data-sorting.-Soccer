using System.ComponentModel.DataAnnotations;

namespace Soccer.Models;

public class Player
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле є обов'язковим для заповнення")]
    [Display(Name = "Ім'я гравця")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Поле є обов'язковим для заповнення")]
    [Display(Name = "Рік народження")]
    public int BirthYear { get; set; }

    [Required(ErrorMessage = "Поле є обов'язковим для заповнення")]
    [Display(Name = "Позиція на полі")]
    public required string Position { get; set; }

    [Required(ErrorMessage = "Поле є обов'язковим для заповнення")]
    [Display(Name = "Команда")]
    public int TeamId { get; set; }

    public Team? Team { get; set; }
}