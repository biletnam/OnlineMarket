using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMarket.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}