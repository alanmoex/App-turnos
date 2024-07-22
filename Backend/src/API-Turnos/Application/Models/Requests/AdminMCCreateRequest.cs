using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class AdminMCCreateRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MedicalCenterId { get; set; }
    }
}
