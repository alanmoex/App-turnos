using Domain.Entities;

namespace Application;

public class MedicDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    public List<Specialty> Specialties {get; set;}

    public static MedicDto Create(Medic medic)
    {
        var dto = new MedicDto();
        dto.Id = medic.Id;
        dto.Name = medic.Name;
        dto.LastName = medic.LastName;
        dto.Specialties = medic.Specialties;

        return dto;
    }

    public static List<MedicDto> CreateList(IEnumerable<Medic> medics)
    {
        List<MedicDto> listDto = new List<MedicDto>();
        foreach (var m in medics)
        {
            listDto.Add(Create(m));
        }

        return listDto;
    }


}
