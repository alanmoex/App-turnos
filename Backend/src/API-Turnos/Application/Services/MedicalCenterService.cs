using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;
using Domain.Exceptions;

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
           ?? throw new NotFoundException(typeof(MedicalCenter).ToString(), id);

        var dto = MedicalCenterDto.Create(obj);

        return dto;

    }

    public MedicalCenterDto Create(MedicalCenterCreateRequest medicalCenterCreateRequest)
    {
        var newMedicalCenter = new MedicalCenter(
            name: medicalCenterCreateRequest.Name
        );

        var obj = _medicalCenterRepository.Add(newMedicalCenter);
        return MedicalCenterDto.Create(obj);
    }

    public void Update(int id, MedicalCenterUpdateRequest medicUpdateRequest)
    {

        var obj = _medicalCenterRepository.GetById(id)
            ?? throw new NotFoundException(typeof(MedicalCenter).ToString(), id);

        obj.Name = medicUpdateRequest.Name;

        _medicalCenterRepository.Update(obj);
    }

    public void Delete(int id)
    {
        var obj = _medicalCenterRepository.GetById(id)
            ?? throw new NotFoundException(typeof(MedicalCenter).ToString(), id);

        _medicalCenterRepository.Delete(obj);
    }


}
