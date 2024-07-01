using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;

namespace Application.Services;

public class SysAdminService : ISysAdminService
{
    private readonly ISysAdminRepository _sysAdminRepository;

    public SysAdminService(ISysAdminRepository sysAdminRepository)
    {
        _sysAdminRepository = sysAdminRepository;
    }

    public SysAdmin Create(SysAdminCreateRequest sysAdminCreateRequest)
    {
        var newSysAdmin = new SysAdmin(sysAdminCreateRequest.Name, sysAdminCreateRequest.Email, sysAdminCreateRequest.Password);
        return _sysAdminRepository.Add(newSysAdmin);
    }

    public void Delete(int id)
    {
        var obj = _sysAdminRepository.GetById(id);

        if (obj == null)
            throw new Exception("");

        _sysAdminRepository.Delete(obj);
    }

    public List<SysAdminDto> GetAll()
    {
        var list = _sysAdminRepository.GetAll();

        return SysAdminDto.CreateList(list);
    }

    public SysAdminDto GetById(int id)
    {
        var obj = _sysAdminRepository.GetById(id)
            ?? throw new Exception("");

        return SysAdminDto.Create(obj);
    }

    public void Update(int id, SysAdminUpdateRequest sysAdminUpdateRequest)
    {
        var obj = _sysAdminRepository.GetById(id)
            ?? throw new Exception("");

        if (sysAdminUpdateRequest.Name != string.Empty) obj.Name = sysAdminUpdateRequest.Name;

        if (sysAdminUpdateRequest.Email != string.Empty) obj.Email = sysAdminUpdateRequest.Email;

        if (sysAdminUpdateRequest.Password != string.Empty) obj.Password = sysAdminUpdateRequest.Password;

        _sysAdminRepository.Update(obj);
    }

}

