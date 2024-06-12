using Domain.Entities;

namespace Application;

public interface ISpecialtyService
{
    List<SpecialtyDto> GetAll();
    SpecialtyDto GetById(int id);
    Specialty Create(SpecialtyCreateRequest specialtyCreateRequest);   
    void Update(int id, SpecialtyUpdateRequest specialtyUpdateRequest);
    void Delete(int id);
}
