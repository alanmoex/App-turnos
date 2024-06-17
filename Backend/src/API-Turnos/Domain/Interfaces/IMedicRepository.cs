using Domain.Entities;
using Domain.Interfaces;

namespace Domain;

public interface IMedicRepository : IBaseRepository<Medic>
{
    List<Specialty> GetMedicSpecialties(int medicId);
}
