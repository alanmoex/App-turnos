using Domain.Entities;

namespace Application;

public class AppointmentDto
{
    public int Id { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public MedicDto Medic { get; set; }

    public PatientDto Patient { get; set; }

    public bool IsCancelled { get; set; }


    public static AppointmentDto Create(Appointment appointment)
    {
        var dto = new AppointmentDto();
        dto.Id = appointment.Id;
        dto.AppointmentDateTime = appointment.AppointmentDateTime;
        dto.Medic = MedicDto.Create(appointment.Medic);
        dto.Patient = PatientDto.Create(appointment.Patient);
        dto.IsCancelled = appointment.IsCancelled;
        return dto;
    }

    public static List<AppointmentDto> CreateList(IEnumerable<Appointment> appointments)
    {
        List<AppointmentDto> listDto = new List<AppointmentDto>();
        foreach (var a in appointments)
        {
            listDto.Add(Create(a));
        }

        return listDto;
    }


}
