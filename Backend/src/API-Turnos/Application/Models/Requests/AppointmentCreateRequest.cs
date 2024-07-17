using System.Collections.Generic;
namespace Application.Models.Requests;


public class AppointmentCreateRequest
{

    public DateTime AppointmentDateTime { get; set; }

    public int MedicId { get; set; }

    public int PatientId { get; set; }

    public int MedicalCenterId { get; set; }

}
