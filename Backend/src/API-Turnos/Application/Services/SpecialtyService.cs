using Application.Models.Requests;
using Domain;
using Domain.Entities;
using Domain.Exceptions;

namespace Application;

public class SpecialtyService : ISpecialtyService
{
    private readonly ISpecialtyRepository _specialtyRepository;
    public SpecialtyService(ISpecialtyRepository specialtyRepository)
    {
        _specialtyRepository = specialtyRepository;
    }

    public SpecialtyDto Create(SpecialtyCreateRequest specialtyCreateRequest)
    {
        var newSpecialty = new Specialty(specialtyCreateRequest.Name);
        var obj = _specialtyRepository.Add(newSpecialty);
        return SpecialtyDto.Create(obj);
    }

    public void Delete(int id)
    {
        var obj = _specialtyRepository.GetById(id)
            ?? throw new NotFoundException(typeof(Specialty).ToString(), id);

        _specialtyRepository.Delete(obj);
    }

    public List<SpecialtyDto> GetAll(){
        var list = _specialtyRepository.GetAll();

        return SpecialtyDto.CreateList(list);
    }

    public SpecialtyDto GetById(int id)
    {
        var obj = _specialtyRepository.GetById(id)
            ?? throw new NotFoundException(typeof(Specialty).ToString(), id);
        
        return SpecialtyDto.Create(obj);
    }

    public void Update(int id,SpecialtyUpdateRequest specialtyUpdateRequest)
    {
        var obj = _specialtyRepository.GetById(id) 
            ?? throw new NotFoundException(typeof(Specialty).ToString(), id);
        
        if (specialtyUpdateRequest.Name != string.Empty) obj.Name = specialtyUpdateRequest.Name;
                
        _specialtyRepository.Update(obj);
    }
}
