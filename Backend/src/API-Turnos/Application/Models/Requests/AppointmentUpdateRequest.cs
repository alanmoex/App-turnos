using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class AppointmentUpdateRequest
    {
        [Required]
        public DateTime AppointmentDateTime { get; set; }
    }
}