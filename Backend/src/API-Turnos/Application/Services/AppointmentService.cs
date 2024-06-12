using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicRepository _medicRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMedicRepository medicRepository, IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _medicRepository = medicRepository ?? throw new ArgumentNullException(nameof(medicRepository));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }
        public Appointment Create(AppointmentCreateRequest appointmentCreateRequest)
        {
            var medic = _medicRepository.GetById(appointmentCreateRequest.MedicId) ?? throw new KeyNotFoundException("Medic not found");
            var patient = _patientRepository.GetById(appointmentCreateRequest.PatientId) ?? throw new KeyNotFoundException("Patient not found");

            var appointment = new Appointment(
                appointmentCreateRequest.AppointmentDateTime, // <-- Aquí falta el argumento appointmentDateTime
                medic,
                patient,
                0 // Esto es un valor ficticio, asegúrate de pasar el ID correcto si es necesario
            )
            {
                IsCancelled = false // Assuming newly created appointments are not cancelled by default
            };

            return _appointmentRepository.Create(appointment);
        }

        public void Delete(int id)
        {
            var appointment = _appointmentRepository.GetById(id) ?? throw new KeyNotFoundException("Appointment not found");

            _appointmentRepository.Delete(appointment);
        }

        public List<Appointment> GetAll()
        {
            return _appointmentRepository.GetAll();
        }

        public Appointment GetById(int id)
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found");
            }
            return appointment;
        }
        public Appointment Update(AppointmentUpdateRequest appointmentUpdateRequest)
        {
            // Verificar si la cita existe
            var existingAppointment = _appointmentRepository.GetById(appointmentUpdateRequest.id);
            if (existingAppointment == null)
            {
                throw new KeyNotFoundException("Appointment not found");
            }

            // Obtener el médico correspondiente
            var medic = _medicRepository.GetById(appointmentUpdateRequest.MedicId);
            if (medic == null)
            {
                throw new KeyNotFoundException("Medic not found");
            }

            // Obtener el paciente correspondiente
            var patient = _patientRepository.GetById(appointmentUpdateRequest.PatientId);
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found");
            }

            // Actualizar los datos de la cita
            existingAppointment.AppointmentDateTime = appointmentUpdateRequest.AppointmentDateTime;
            existingAppointment.Medic = medic;
            existingAppointment.Patient = patient;
            existingAppointment.IsCancelled = appointmentUpdateRequest.IsCancelled; // Utilizar la propiedad IsCancelled

            // Actualizar la cita en el repositorio
            return _appointmentRepository.Update(existingAppointment);
        }
    }
}