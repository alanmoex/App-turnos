using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using System.Collections.Generic;
using System;
using Domain;
using System.Transactions;
using Domain.Enums;
using Domain.Exceptions;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicRepository _medicRepository;
        private readonly IMedicalCenterRepository _medicalCenterRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMedicRepository medicRepository, IMedicalCenterRepository medicalCenterRepository, IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _medicRepository = medicRepository;
            _medicalCenterRepository = medicalCenterRepository;
            _patientRepository = patientRepository;
        }

        public AppointmentDto? GetById(int id)
        {
            var appointment = _appointmentRepository.GetById(id)
                ?? throw new NotFoundException(typeof(Appointment).ToString(), id);
            return AppointmentDto.Create(appointment);
        }

        public List<AppointmentDto> GetAll()
        {
            var list = _appointmentRepository.GetAll();
            return AppointmentDto.CreateList(list);
        }

        public AppointmentDto Create(AppointmentCreateRequest appointmentCreateRequest)
        {
            var medic = _medicRepository.GetById(appointmentCreateRequest.MedicId)
                ?? throw new NotFoundException(typeof(Medic).ToString(), appointmentCreateRequest.MedicId);
            var medicalCenter = _medicalCenterRepository.GetById(appointmentCreateRequest.MedicalCenterId)
                ?? throw new NotFoundException(typeof(MedicalCenter).ToString(), appointmentCreateRequest.MedicalCenterId);

            var newAppointment = new Appointment(
                appointmentDateTime: appointmentCreateRequest.AppointmentDateTime,
                medic: medic,
                medicalCenter: medicalCenter
            );

            var appointment = _appointmentRepository.Add(newAppointment);
            return AppointmentDto.Create(appointment);
        }

        public void Update(int id, AppointmentUpdateRequest appointmentUpdateRequest)
        {
            var appointment = _appointmentRepository.GetById(id)
                ?? throw new NotFoundException(typeof(Appointment).ToString(), id);

            appointment.AppointmentDateTime = appointmentUpdateRequest.AppointmentDateTime;

            _appointmentRepository.Update(appointment);
        }

        public void Delete(int id)
        {
            var appointment = _appointmentRepository.GetById(id)
                ?? throw new NotFoundException(typeof(Appointment).ToString(), id);
            _appointmentRepository.Delete(appointment);
        }

        public void CheckAndCreateAutomaticAppointments()
        {
            var currentDateUtc = DateTime.UtcNow;

            // Calcular la fecha objetivo para la creación de turnos (una semana antes del primer día del próximo mes)
            var nextMonth = new DateTime(currentDateUtc.Year, currentDateUtc.Month, 1).AddMonths(1);
            var targetDate = nextMonth.AddDays(-7).Date;

            // Verificar si hoy es la fecha objetivo
            if (currentDateUtc.Date == targetDate)
            {
                CreateAutomaticAppointments();
            }
        }

        public void CreateAutomaticAppointments()
        {
            var currentDateUtc = DateTime.UtcNow;

            // Obtener el mes y año del próximo mes
            var nextMonth = currentDateUtc.AddMonths(1);
            var year = nextMonth.Year;
            var month = nextMonth.Month;

            var allMedics = _medicRepository.GetAll();

            // Utilizar TransactionScope para manejar la transacción
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                               new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                foreach (var medic in allMedics)
                {
                    foreach (var workSchedule in medic.WorkSchedules)
                    {
                        // Generar turnos para cada día del próximo mes según el horario del médico
                        var startDate = new DateTime(year, month, 1);
                        var endDate = startDate.AddMonths(1).AddDays(-1); // Último día del próximo mes
                        var currentDate = startDate;

                        while (currentDate <= endDate)
                        {
                            // Verificar si es un día hábil según el horario del médico
                            if (workSchedule.Day == currentDate.DayOfWeek)
                            {
                                // Crear turnos cada 30 minutos dentro del horario del médico
                                var appointmentTime = workSchedule.StartTime;
                                while (appointmentTime < workSchedule.EndTime)
                                {
                                    var appointmentDateTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, appointmentTime.Hours, appointmentTime.Minutes, 0, DateTimeKind.Utc);

                                    // Verificar si ya existe un turno para este médico, fecha y centro médico
                                    if (!_appointmentRepository.Exists(appointmentDateTime, medic.Id, medic.MedicalCenter.Id))
                                    {
                                        var newAppointment = new Appointment(appointmentDateTime, medic, medic.MedicalCenter);

                                        // Agregar el Appointment
                                        _appointmentRepository.Add(newAppointment);
                                    }

                                    // Avanzar 30 minutos
                                    appointmentTime = appointmentTime.Add(new TimeSpan(0, 30, 0));
                                }
                            }

                            // Avanzar al siguiente día
                            currentDate = currentDate.AddDays(1);
                        }
                    }
                }

                transactionScope.Complete();
            }
        }

        public void TakeAppointment(int appointmentId, int patientId)
        {
            var appointment = _appointmentRepository.GetById(appointmentId)
                             ?? throw new NotFoundException(typeof(Appointment).ToString(), appointmentId);

            if (appointment.Status != AppointmentStatus.Available)
            {
                throw new InvalidOperationException("The appointment is not available.");
            }

            var patient = _patientRepository.GetById(patientId)
                           ?? throw new NotFoundException(typeof(Patient).ToString(), patientId);

            appointment.Patient = patient;
            appointment.Status = AppointmentStatus.Taken;

            _appointmentRepository.Update(appointment);
        }

        public void CancelAppointment(int appointmentId, int patientId)
        {
            var appointment = _appointmentRepository.GetById(appointmentId)
                             ?? throw new NotFoundException(typeof(Appointment).ToString(), appointmentId);

            if (appointment.Status != AppointmentStatus.Taken || appointment.Patient?.Id != patientId)
            {
                throw new InvalidOperationException("The appointment cannot be canceled by this patient.");
            }

            var patient = _patientRepository.GetById(patientId)
                           ?? throw new NotFoundException(typeof(Patient).ToString(), patientId);

            appointment.Patient = null;
            appointment.Status = AppointmentStatus.Available;

            _appointmentRepository.Update(appointment);
        }

        public List<AppointmentDto> GetAppointmentsByMedicalCenterId(int medicalCenterId)
        {
            var appointments = _appointmentRepository.GetByMedicalCenterId(medicalCenterId);
            return AppointmentDto.CreateList(appointments);
        }
    }
}
