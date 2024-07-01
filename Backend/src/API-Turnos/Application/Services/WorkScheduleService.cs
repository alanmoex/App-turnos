﻿using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;

namespace Application.Services;

public class WorkScheduleService : IWorkScheduleService
{
    private readonly IWorkScheduleRepository _workScheduleRepository;
    private readonly IMedicRepository _medicRepository;
    public WorkScheduleService(IWorkScheduleRepository workScheduleRepository, IMedicRepository medicRepository)
    {
        _workScheduleRepository = workScheduleRepository;
        _medicRepository = medicRepository;
    }

    public WorkSchedule Create(WorkScheduleCreateRequest workScheduleCreateRequest)
    {
        var startTime = TimeSpan.Parse(workScheduleCreateRequest.StartTime);
        var endTime = TimeSpan.Parse(workScheduleCreateRequest.EndTime);

        var newWorkSchedule = new WorkSchedule(workScheduleCreateRequest.Day, startTime, endTime);
        return _workScheduleRepository.Add(newWorkSchedule);
    }

    public void Delete(int id)
    {
        var obj = _workScheduleRepository.GetById(id);

        if (obj == null)
            throw new Exception("");

        _workScheduleRepository.Delete(obj);
    }

    public List<WorkScheduleDto> GetAll(){
        var list = _workScheduleRepository.GetAll();

        return WorkScheduleDto.CreateList(list);
    }

    public WorkScheduleDto GetById(int id)
    {
        var obj = _workScheduleRepository.GetById(id)
            ?? throw new Exception("");
        
        return WorkScheduleDto.Create(obj);
    }

    public void Update(int id,WorkScheduleUpdateRequest workScheduleUpdateRequest)
    {
        var obj = _workScheduleRepository.GetById(id) 
            ?? throw new Exception("");
        var startTime = TimeSpan.Parse(workScheduleUpdateRequest.StartTime);
        var endTime = TimeSpan.Parse(workScheduleUpdateRequest.EndTime);
        
          if (Enum.IsDefined(typeof(DayOfWeek), workScheduleUpdateRequest.Day))
            {
                obj.Day = (DayOfWeek)workScheduleUpdateRequest.Day;
            }
            else
                {
                    throw new ArgumentException("Invalid day of the week.");
                }

       
        if (workScheduleUpdateRequest.StartTime != string.Empty) obj.StartTime = startTime;

        if (workScheduleUpdateRequest.EndTime != string.Empty) obj.EndTime = endTime;
        
        
        _workScheduleRepository.Update(obj);
    }

}