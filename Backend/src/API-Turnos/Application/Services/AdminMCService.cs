using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;

namespace Application.Services;

public class AdminMCService : IAdminMCService
{
    private readonly IAdminMCRepository _adminMCRepository;
    private readonly IMedicalCenterRepository _medicalCenterRepository;
    public AdminMCService(IAdminMCRepository adminMCRepository, IMedicalCenterRepository medicalCenterRepository)
    {
        _adminMCRepository = adminMCRepository;
        _medicalCenterRepository = medicalCenterRepository;
    }

    public AdminMC Create(AdminMCCreateRequest adminMCCreateRequest)
    {
        var medicalCenter = _medicalCenterRepository.GetById(adminMCCreateRequest.MedicalCenterId)
            ?? throw new Exception("MedicalCenter not found.");
        var newAdminMC = new AdminMC(adminMCCreateRequest.Name, adminMCCreateRequest.Email, adminMCCreateRequest.Password, medicalCenter);
        return _adminMCRepository.Add(newAdminMC);
    }

    public void Delete(int id)
    {
        var obj = _adminMCRepository.GetById(id);

        if (obj == null)
            throw new Exception("");

        _adminMCRepository.Delete(obj);
    }

    public List<AdminMCDto> GetAll(){
        var list = _adminMCRepository.GetAll();

        return AdminMCDto.CreateList(list);
    }

    public AdminMCDto GetById(int id)
    {
        var obj = _adminMCRepository.GetById(id)
            ?? throw new Exception("");
        
        return AdminMCDto.Create(obj);
    }

    public async Task<AdminMCDto> GetByIdAsync(int id)
    {
        var obj = await _adminMCRepository.GetByIdAsync(id)
            ?? throw new Exception("");

        return AdminMCDto.Create(obj);
    }

    public void Update(int id,AdminMCUpdateRequest adminMCUpdateRequest)
    {
        var obj = _adminMCRepository.GetById(id) 
            ?? throw new Exception("");
        
        if (adminMCUpdateRequest.Name != string.Empty) obj.Name = adminMCUpdateRequest.Name;

        if (adminMCUpdateRequest.Email != string.Empty) obj.Email = adminMCUpdateRequest.Email;

        if (adminMCUpdateRequest.Password != string.Empty) obj.Password = adminMCUpdateRequest.Password;
        
        _adminMCRepository.Update(obj);
    }

}
