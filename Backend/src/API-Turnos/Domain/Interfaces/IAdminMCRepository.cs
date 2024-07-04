using Domain.Entities;
using Domain.Interfaces;

namespace Domain;

public interface IAdminMCRepository : IBaseRepository<AdminMC>
{
    Task<AdminMC?> GetByIdAsync<TId>(TId id);
}
