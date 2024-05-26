using System.ComponentModel.DataAnnotations;

namespace UserApplication.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        public string Cellphone { get; set; }
    }
}
