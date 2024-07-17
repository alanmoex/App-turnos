using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using System.Collections.Generic;
using System;
using Domain;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicRepository _medicRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicalCenterRepository _medicalCenterRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMedicRepository medicRepository, IPatientRepository patientRepository, IMedicalCenterRepository medicalCenterRepository)
        {
            _appointmentRepository = appointmentRepository;
            _medicRepository = medicRepository;
            _patientRepository = patientRepository;
            _medicalCenterRepository = medicalCenterRepository;
        }

        public AppointmentDto? GetById(int id)
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                throw new Exception("Appointment not found.");
            }
            var dto = AppointmentDto.Create(appointment);
            return dto;
        }

        public List<AppointmentDto> GetAll()
        {
            var list = _appointmentRepository.GetAll();
            return AppointmentDto.CreateList(list);
        }

        public Appointment Create(AppointmentCreateRequest appointmentCreateRequest)
        {
            var medic = _medicRepository.GetById(appointmentCreateRequest.MedicId)
                            ?? throw new Exception("Medic not found.");
            var patient = _patientRepository.GetById(appointmentCreateRequest.PatientId)
                            ?? throw new Exception("Patient not found.");
            var medicalCenter = _medicalCenterRepository.GetById(appointmentCreateRequest.MedicalCenterId)
                            ?? throw new Exception("Medical Center not found.");

            var newAppointment = new Appointment(
                appointmentDateTime: appointmentCreateRequest.AppointmentDateTime,
                medic: medic,
                patient: patient,
                medicalCenter: medicalCenter
            );

            return _appointmentRepository.Add(newAppointment);
        }

        public void Update(int id, AppointmentUpdateRequest appointmentUpdateRequest)
        {
            var appointment = _appointmentRepository.GetById(id)
                             ?? throw new Exception("Appointment not found.");

            // Verificar si appointmentUpdateRequest es null
            if (appointmentUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(appointmentUpdateRequest), "Appointment update request cannot be null.");
            }

            // Actualizar la fecha y hora de la cita si se proporciona en la solicitud de actualización
            if (appointmentUpdateRequest.AppointmentDateTime != default)
            {
                appointment.AppointmentDateTime = appointmentUpdateRequest.AppointmentDateTime;
            }

            // Llamar al método de repositorio para actualizar el appointment
            _appointmentRepository.Update(appointment);
            
        }
        public void Delete(int id)
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                throw new Exception("Appointment not found.");
            }
            _appointmentRepository.Delete(appointment);
        }
    }
}