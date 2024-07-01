using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces;

public interface ISysAdminService
{
    List<SysAdminDto> GetAll();
    SysAdminDto GetById(int id);
    SysAdmin Create(SysAdminCreateRequest sysAdminCreateRequest);
    void Update(int id, SysAdminUpdateRequest sysAdminUpdateRequest);
    void Delete(int id);
}