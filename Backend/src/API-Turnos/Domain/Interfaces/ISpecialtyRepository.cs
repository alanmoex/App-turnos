using Domain.Entities;

namespace Domain;

public interface ISpecialtyRepository
{
    Specialty? GetById(int id);
    List<Specialty> GetAll();
    Specialty Add(Specialty specialty);
    Specialty Update(Specialty specialty);
    void Delete(Specialty specialty);
}