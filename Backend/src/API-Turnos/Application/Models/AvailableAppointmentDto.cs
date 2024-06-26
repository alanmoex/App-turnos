using Domain.Entities;

namespace Application;

public class AvailableAppointmentDto
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public Medic Medic { get; set; }

    public static AvailableAppointmentDto Create(AvailableAppointment availableAppointment)
    {
        var dto = new AvailableAppointmentDto();
        dto.Id = availableAppointment.Id;
        dto.DateTime = availableAppointment.DateTime;
        dto.Medic = availableAppointment.Medic;

        return dto;
    }


    public static List<AvailableAppointmentDto> CreateList(IEnumerable<AvailableAppointment> availableAppointments)
    {
        List<AvailableAppointmentDto> listDto = new List<AvailableAppointmentDto>();
        foreach (var a in availableAppointments)
        {
            listDto.Add(Create(a));
        }

        return listDto;
    }

}


