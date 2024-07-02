using Domain.Entities;

namespace Application;

public class SysAdminDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public static SysAdminDto Create(SysAdmin sysAdmin)
    {
        var dto = new SysAdminDto();
        dto.Id = sysAdmin.Id;
        dto.Name = sysAdmin.Name;
        dto.Email = sysAdmin.Email;

        return dto;
    }

    public static List<SysAdminDto> CreateList(IEnumerable<SysAdmin> sysAdmins)
    {
        List<SysAdminDto> listDto = new List<SysAdminDto>();
        foreach (var s in sysAdmins)
        {
            listDto.Add(Create(s));
        }

        return listDto;
    }

}
