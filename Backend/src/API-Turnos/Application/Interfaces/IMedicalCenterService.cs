using Application.Models;
using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces;

public interface IMedicalCenterService
{
    List<MedicalCenterDto> GetAll();
    MedicalCenterDto GetById(int id);
    MedicalCenter Create(MedicalCenterCreateRequest medicCreateRequest);
    void Update(int id, MedicalCenterUpdateRequest medicUpdateRequest);
    void Delete(int id);
}