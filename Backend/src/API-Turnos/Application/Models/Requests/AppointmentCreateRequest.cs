using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class AppointmentCreateRequest
    {
        [Required]
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MedicId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MedicalCenterId { get; set; }
    }
}