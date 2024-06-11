using Domain.Entities;

namespace Domain;

public interface IMedicRepository
{
    Medic? GetById(int id);
    List<Medic> GetAll();
    Medic Add(Medic medic);
    Medic Update(Medic medic);
    void Delete(Medic medic);
}