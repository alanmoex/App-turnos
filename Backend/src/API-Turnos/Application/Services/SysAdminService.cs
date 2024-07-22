using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class SysAdminService : ISysAdminService
{
    private readonly ISysAdminRepository _sysAdminRepository;

    public SysAdminService(ISysAdminRepository sysAdminRepository)
    {
        _sysAdminRepository = sysAdminRepository;
    }

    public SysAdminDto Create(SysAdminCreateRequest sysAdminCreateRequest)
    {
        var newSysAdmin = new SysAdmin(sysAdminCreateRequest.Name, sysAdminCreateRequest.Email, sysAdminCreateRequest.Password);
        var obj = _sysAdminRepository.Add(newSysAdmin);
        return SysAdminDto.Create(obj);
    }

    public void Delete(int id)
    {
        var obj = _sysAdminRepository.GetById(id)
            ?? throw new NotFoundException(typeof(SysAdmin).ToString(), id);

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
            ?? throw new NotFoundException(typeof(SysAdmin).ToString(), id);

        return SysAdminDto.Create(obj);
    }

    public void Update(int id, SysAdminUpdateRequest sysAdminUpdateRequest)
    {
        var obj = _sysAdminRepository.GetById(id)
            ?? throw new NotFoundException(typeof(SysAdmin).ToString(), id);

        if (sysAdminUpdateRequest.Name != null) obj.Name = sysAdminUpdateRequest.Name;

        if (sysAdminUpdateRequest.Email != null) obj.Email = sysAdminUpdateRequest.Email;

        if (sysAdminUpdateRequest.Password != null) obj.Password = sysAdminUpdateRequest.Password;

        _sysAdminRepository.Update(obj);
    }

}

