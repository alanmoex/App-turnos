using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class SpecialtyUpdateRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
