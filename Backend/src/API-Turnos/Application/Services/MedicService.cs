using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;

namespace Application.Services;

public class MedicService : IMedicService
{
    private readonly IMedicRepository _medicRepository;
    public MedicService(IMedicRepository medicRepository)
    {
        _medicRepository = medicRepository;
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
        foreach (var specialtyName in medicCreateRequest.Speciality)
        {
            specialties.Add(new Specialty(specialtyName));
        }

        var newMedic = new Medic(
            id: 0, // O algún valor predeterminado o generado automáticamente
            name: medicCreateRequest.Name,
            lastName: medicCreateRequest.LastName,
            licenseNumber: medicCreateRequest.LicenseNumber,
            specialties: specialties // Utiliza la propiedad Specialties
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

        if (medicUpdateRequest.Speciality != null)
        {
            var specialties = new List<Specialty>();
            foreach (var specialtyName in medicUpdateRequest.Speciality)
            {
                specialties.Add(new Specialty(specialtyName));
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
