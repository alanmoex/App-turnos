using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;

namespace Infrastructure.Data;

public class MedicRepository : IMedicRepository
{
    static int LastIdAssigned = 0;
    static List<Medic> medics = [];

    public Medic Add(Medic medic)
    {
        medic.Id = ++LastIdAssigned;
        medics.Add(medic);
        return medic;
    }

    public List<Medic> GetAll()
    {
        return medics;
    }

    public Medic? GetById(int id)
    {
        return medics.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(Medic medic)
    {
        medics.Remove(medic);
    }

    public Medic Update(Medic medic)
    {
         var obj = medics.FirstOrDefault(m => m.Id == medic.Id) ?? throw new Exception();
        obj.Name = medic.Name;
        return obj;
    }
}