using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAdminMCService
{
    List<AdminMCDto> GetAll();
    AdminMCDto GetById(int id);
    AdminMCDto Create(AdminMCCreateRequest adminMCCreateRequest);   
    void Update(int id, AdminMCUpdateRequest adminMCUpdateRequest);
    void Delete(int id);
}