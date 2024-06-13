namespace Application;

public class MedicUpdateRequest
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string LicenseNumber { get; set; }
    public List<int> Specialties { get; set; }
}
