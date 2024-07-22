using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class WorkScheduleCreateRequest
    {
        [Required]

        public DayOfWeek Day { get; set; }

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$",
            ErrorMessage = "The time must be in the format HH:mm:ss")]
        public string StartTime { get; set; }

        [Required]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$",
            ErrorMessage = "The time must be in the format HH:mm:ss")]
        public string EndTime { get; set; }
    }
}
