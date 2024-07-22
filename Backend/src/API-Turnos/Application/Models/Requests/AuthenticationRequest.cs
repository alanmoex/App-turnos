using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class AuthenticationRequest
    {
        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Password { get; set; }
    }
}
