using Application.Models;
using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces;

public interface IMedicService
{
    List<MedicDto> GetAll();
    MedicDto GetById(int id);
    Medic Create(MedicCreateRequest medicCreateRequest);   
    void Update(int id, MedicUpdateRequest medicUpdateRequest);
    void Delete(int id);
}