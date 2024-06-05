using Domain.Entities;

namespace Application;

public class PatientDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    public static PatientDto Create(Patient patient)
    {
        var dto = new PatientDto();
        dto.Id = patient.Id;
        dto.Name = patient.Name;
        dto.LastName = patient.LastName;

        return dto;
    }

    public static List<PatientDto> CreateList(IEnumerable<Patient> patients)
    {
        List<PatientDto> listDto = new List<PatientDto>();
        foreach (var p in patients)
        {
            listDto.Add(Create(p));
        }

        return listDto;
    }


}
