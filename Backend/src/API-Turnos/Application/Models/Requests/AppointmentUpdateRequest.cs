using System.Collections.Generic;
namespace Application.Models.Requests;


public class AppointmentUpdateRequest
{
    public int id { get; set; }
    public DateTime AppointmentDateTime { get; set; }

    public int MedicId { get; set; }

    public int PatientId { get; set; }

    public bool IsCancelled { get; set; }

}
