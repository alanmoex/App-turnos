using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class MedicUpdateRequest
    {
        [StringLength(100, MinimumLength = 1)]
        public string? Name { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string? LastName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string? LicenseNumber { get; set; }
    }
}
