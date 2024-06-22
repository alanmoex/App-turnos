using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;

namespace Application.Services;

public class MedicalCenterService : IMedicalCenterService
{
    private readonly IMedicalCenterRepository _medicalCenterRepository;
    public MedicalCenterService(IMedicalCenterRepository medicalCenterRepository)
    {
        _medicalCenterRepository = medicalCenterRepository;
    }

    public List<MedicalCenterDto> GetAll()
    {
        var list = _medicalCenterRepository.GetAll();
        return MedicalCenterDto.CreateList(list);
    }

    public MedicalCenterDto GetById(int id)
    {
        var obj = _medicalCenterRepository.GetById(id)
           ?? throw new Exception("");

        var dto = MedicalCenterDto.Create(obj);

        return dto;

    }

    public MedicalCenter Create(MedicalCenterCreateRequest medicalCenterCreateRequest)
    {
        var newMedicalCenter = new MedicalCenter(
            name: medicalCenterCreateRequest.Name
        );

        return _medicalCenterRepository.Add(newMedicalCenter);
    }

    public void Update(int id, MedicalCenterUpdateRequest medicUpdateRequest)
    {

        var obj = _medicalCenterRepository.GetById(id)
         ?? throw new Exception("");
        if (medicUpdateRequest.Name != string.Empty) obj.Name = medicUpdateRequest.Name;

        _medicalCenterRepository.Update(obj);
    }

    public void Delete(int id)
    {
        var obj = _medicalCenterRepository.GetById(id);
        if (obj == null)
        {
            throw new Exception("");
        }
        _medicalCenterRepository.Delete(obj);
    }


}
