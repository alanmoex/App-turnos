using Domain;
using Domain.Entities;

namespace Application;

public class SpecialtyService : ISpecialtyService
{
    private readonly ISpecialtyRepository _specialtyRepository;
    private readonly IMedicRepository _medicRepository;
    public SpecialtyService(ISpecialtyRepository specialtyRepository, IMedicRepository medicRepository)
    {
        _specialtyRepository = specialtyRepository;
        _medicRepository = medicRepository;
    }

    public Specialty Create(SpecialtyCreateRequest specialtyCreateRequest)
    {
        var newSpecialty = new Specialty(specialtyCreateRequest.Name);
        return _specialtyRepository.Add(newSpecialty);
    }

    public void Delete(int id)
    {
        var obj = _specialtyRepository.GetById(id);

        if (obj == null)
            throw new Exception("");

        _specialtyRepository.Delete(obj);
    }

    public List<SpecialtyDto> GetAll(){
        var list = _specialtyRepository.GetAll();

        return SpecialtyDto.CreateList(list);
    }

    public SpecialtyDto GetById(int id)
    {
        var obj = _specialtyRepository.GetById(id)
            ?? throw new Exception("");
        
        return SpecialtyDto.Create(obj);
    }

    public void Update(int id,SpecialtyUpdateRequest specialtyUpdateRequest)
    {
        var obj = _specialtyRepository.GetById(id) 
            ?? throw new Exception("");
        
        if (specialtyUpdateRequest.Name != string.Empty) obj.Name = specialtyUpdateRequest.Name;
                
        _specialtyRepository.Update(obj);
    }
}
