using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class MedicService : IMedicService
{
    private readonly IMedicRepository _medicRepository;
    private readonly ISpecialtyRepository _specialtyRepository;
    private readonly IMedicalCenterRepository _medicalCenterRepository;
    public MedicService(IMedicRepository medicRepository, ISpecialtyRepository specialtyRepository, IMedicalCenterRepository medicalCenterRepository)
    {
        _medicRepository = medicRepository;
        _specialtyRepository = specialtyRepository;
        _medicalCenterRepository = medicalCenterRepository;
    }

    public List<MedicDto> GetAll()
    {
        var list = _medicRepository.GetAll();
        return MedicDto.CreateList(list);
    }

    public MedicDto GetById(int id)
    {
        var obj = _medicRepository.GetById(id)
           ?? throw new NotFoundException(typeof(Medic).ToString(), id);

        var dto = MedicDto.Create(obj);

        return dto;

    }

    public MedicDto Create(MedicCreateRequest medicCreateRequest)
    {
        var specialties = new List<Specialty>();
        foreach (var specialtyId in medicCreateRequest.Specialties)
        {
            var specialty = _specialtyRepository.GetById(specialtyId);
            if (specialty != null)
            {
                specialties.Add(specialty);
            }
        }

        if (!specialties.Any())
        {
            throw new NotFoundException("No specialties found with the provided IDs.");
        }

        var medicalCenter = _medicalCenterRepository.GetById(medicCreateRequest.MedicalCenterId)
                         ?? throw new NotFoundException(typeof(MedicalCenter).ToString(), medicCreateRequest.MedicalCenterId);

        var newMedic = new Medic(

            name: medicCreateRequest.Name,
            lastName: medicCreateRequest.LastName,
            licenseNumber: medicCreateRequest.LicenseNumber,
            medicalCenter: medicalCenter,
            specialties: specialties
        );

        var obj = _medicRepository.Add(newMedic);
        return MedicDto.Create(obj);
    }

    public void Update(int id, MedicUpdateRequest medicUpdateRequest)
    {

        var obj = _medicRepository.GetById(id)
            ?? throw new NotFoundException(typeof(Medic).ToString(), id);

        if (medicUpdateRequest.Name != null) obj.Name = medicUpdateRequest.Name;

        if (medicUpdateRequest.LastName != null) obj.LastName = medicUpdateRequest.LastName;

        if (medicUpdateRequest.LicenseNumber != null) obj.LicenseNumber = medicUpdateRequest.LicenseNumber;

        _medicRepository.Update(obj);
    }

    public void Delete(int id)
    {
        var obj = _medicRepository.GetById(id)
            ?? throw new NotFoundException(typeof(Medic).ToString(), id);

        _medicRepository.Delete(obj);
    }


}
