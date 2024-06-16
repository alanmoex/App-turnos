using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class WorkSchedule
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    // Constructor sin parámetros necesario para EF
    public WorkSchedule()
    {
    }

    public WorkSchedule(DayOfWeek day, TimeSpan startTime, TimeSpan endTime)
    {
        Day = day;
        StartTime = startTime;
        EndTime = endTime;
    }
}
