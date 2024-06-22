using Domain.Entities;

namespace Application;

public class MedicalCenterDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<MedicDto> Medics { get; set; }

    public static MedicalCenterDto Create(MedicalCenter medicalCenter)
    {
        var dto = new MedicalCenterDto();
        dto.Id = medicalCenter.Id;
        dto.Name = medicalCenter.Name;
        dto.Medics = MedicDto.CreateList(medicalCenter.Medics);

        return dto;
    }

    public static List<MedicalCenterDto> CreateList(IEnumerable<MedicalCenter> medicalCenters)
    {
        List<MedicalCenterDto> listDto = new List<MedicalCenterDto>();
        if (medicalCenters != null)
        {
            foreach (var mc in medicalCenters)
            {
                listDto.Add(Create(mc));
            }

        }

        return listDto;
    }


}
