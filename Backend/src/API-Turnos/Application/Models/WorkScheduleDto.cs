using Domain.Entities;

namespace Application;

public class WorkScheduleDto
{
    public int Id {get;set;}
    public  DayOfWeek Day {get;set;}
    public TimeSpan StartTime {get;set;}
    public TimeSpan EndTime {get;set;}

    public static WorkScheduleDto Create(WorkSchedule workSchedule)
    {
        var dto = new WorkScheduleDto();
        dto.Id = workSchedule.Id;
        dto.Day = workSchedule.Day;
        dto.StartTime= workSchedule.StartTime;
        dto.EndTime = workSchedule.EndTime;

        return dto;
    }

    public static List<WorkScheduleDto> CreateList(IEnumerable<WorkSchedule> workSchedules)
    {
        List<WorkScheduleDto> listDto = new List<WorkScheduleDto>();
        foreach (var w in workSchedules)
        {
            listDto.Add(Create(w));
        }

        return listDto;
    }

}