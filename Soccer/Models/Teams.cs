using System.ComponentModel.DataAnnotations;

namespace Soccer.Models
{
    public class Teams
    {
        public Teams()
        {
            this.Players = new HashSet<Players>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "The field must be set")]
        [Display(Name = "Club name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field must be set")]
        [Display(Name = "Club coach")]
        public string Coach { get; set; }
        public ICollection<Players>? Players { get; set; }
    }
}
