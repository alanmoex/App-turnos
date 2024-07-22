using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class MedicCreateRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string LicenseNumber { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MedicalCenterId { get; set; }

        [Required]
        public List<int> Specialties { get; set; }
    }
}
