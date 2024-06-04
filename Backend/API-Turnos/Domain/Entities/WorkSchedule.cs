namespace Domain.Entities;

public class WorkSchedule
{
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public WorkSchedule(DayOfWeek day, TimeSpan startTime, TimeSpan endTime)
    {
        Day = day;
        StartTime = startTime;
        EndTime = endTime;
    }
}
