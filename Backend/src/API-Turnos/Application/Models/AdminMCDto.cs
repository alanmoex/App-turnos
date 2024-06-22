using Domain.Entities;

namespace Application;

public class AdminMCDto
{
    public int Id {get;set;}
    public string Name {get;set;}
    public string Email {get;set;}
    public string Password {get;set;}

    public static AdminMCDto Create(AdminMC adminMC)
    {
        var dto = new AdminMCDto();
        dto.Id = adminMC.Id;
        dto.Name = adminMC.Name;
        dto.Email= adminMC.Email;
        dto.Password = adminMC.Password;

        return dto;
    }

    public static List<AdminMCDto> CreateList(IEnumerable<AdminMC> adminMCs)
    {
        List<AdminMCDto> listDto = new List<AdminMCDto>();
        foreach (var a in adminMCs)
        {
            listDto.Add(Create(a));
        }

        return listDto;
    }

}
