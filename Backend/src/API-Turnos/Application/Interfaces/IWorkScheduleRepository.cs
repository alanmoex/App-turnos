using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces;

public interface IWorkScheduleService
{
    List<WorkScheduleDto> GetAll();
    WorkScheduleDto GetById(int id);
    WorkScheduleDto Create(WorkScheduleCreateRequest workScheduleCreateRequest);   
    void Update(int id, WorkScheduleUpdateRequest workScheduleUpdateRequest);
    void Delete(int id);
}
