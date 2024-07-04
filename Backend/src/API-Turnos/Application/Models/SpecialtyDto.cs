using Domain.Entities;

namespace Application;

public class SpecialtyDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static SpecialtyDto Create(Specialty specialty)
    {
        var dto = new SpecialtyDto();
        dto.Id = specialty.Id;
        dto.Name = specialty.Name;
        return dto;
    }

    public static List<SpecialtyDto> CreateList(IEnumerable<Specialty> specialtys)
    {
        List<SpecialtyDto> listDto = new List<SpecialtyDto>();
        foreach (var s in specialtys)
        {
            listDto.Add(Create(s));
        }

        return listDto;
    }
}
