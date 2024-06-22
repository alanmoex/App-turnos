using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;

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
           ?? throw new Exception("");

        var dto = MedicDto.Create(obj);

        return dto;

    }

    public Medic Create(MedicCreateRequest medicCreateRequest)
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

        var medicalCenter = _medicalCenterRepository.GetById(medicCreateRequest.MedicalCenterId)
                         ?? throw new Exception("Medic not found.");

        var newMedic = new Medic(

            name: medicCreateRequest.Name,
            lastName: medicCreateRequest.LastName,
            licenseNumber: medicCreateRequest.LicenseNumber,
            medicalCenter: medicalCenter,
            specialties: specialties
        );

        return _medicRepository.Add(newMedic);
    }

    public void Update(int id, MedicUpdateRequest medicUpdateRequest)
    {

        var obj = _medicRepository.GetById(id)
         ?? throw new Exception("");
        if (medicUpdateRequest.Name != string.Empty) obj.Name = medicUpdateRequest.Name;

        if (medicUpdateRequest.LastName != string.Empty) obj.LastName = medicUpdateRequest.LastName;

        if (medicUpdateRequest.LicenseNumber != string.Empty) obj.LicenseNumber = medicUpdateRequest.LicenseNumber;

        if (medicUpdateRequest.Specialties != null)
        {
            var specialties = new List<Specialty>();
            foreach (var specialtyId in medicUpdateRequest.Specialties)
            {
                var specialty = _specialtyRepository.GetById(specialtyId);
                if (specialty != null)
                {
                    specialties.Add(specialty);
                }
            }
            obj.Specialties = specialties;
        }

        _medicRepository.Update(obj);
    }

    public void Delete(int id)
    {
        var obj = _medicRepository.GetById(id);
        if (obj == null)
        {
            throw new Exception("");
        }
        _medicRepository.Delete(obj);
    }


}
