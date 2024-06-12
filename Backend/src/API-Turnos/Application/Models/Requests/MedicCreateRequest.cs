using System.Collections.Generic;
namespace Application.Models.Requests;


public class MedicCreateRequest
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string LicenseNumber { get; set; }
    public List<string> Speciality { get; set; }
}
