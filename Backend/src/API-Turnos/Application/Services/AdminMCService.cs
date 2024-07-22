using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    public class AdminMCService : IAdminMCService
    {
        private readonly IAdminMCRepository _adminMCRepository;
        private readonly IMedicalCenterRepository _medicalCenterRepository;
        public AdminMCService(IAdminMCRepository adminMCRepository, IMedicalCenterRepository medicalCenterRepository)
        {
            _adminMCRepository = adminMCRepository;
            _medicalCenterRepository = medicalCenterRepository;
        }

        public AdminMCDto Create(AdminMCCreateRequest adminMCCreateRequest)
        {
            var medicalCenter = _medicalCenterRepository.GetById(adminMCCreateRequest.MedicalCenterId)
                ?? throw new NotFoundException(typeof(MedicalCenter).ToString(), adminMCCreateRequest.MedicalCenterId);

            var newAdminMC = new AdminMC(adminMCCreateRequest.Name, adminMCCreateRequest.Email, adminMCCreateRequest.Password, medicalCenter);
            var adminMc = _adminMCRepository.Add(newAdminMC);

            return AdminMCDto.Create(adminMc);
        }

        public void Delete(int id)
        {
            var obj = _adminMCRepository.GetById(id)
                ?? throw new NotFoundException(typeof(AdminMC).ToString(), id);

            _adminMCRepository.Delete(obj);
        }

        public List<AdminMCDto> GetAll()
        {
            var list = _adminMCRepository.GetAll();
            return AdminMCDto.CreateList(list);
        }

        public AdminMCDto GetById(int id)
        {
            var obj = _adminMCRepository.GetById(id)
                ?? throw new NotFoundException(typeof(AdminMC).ToString(), id);

            return AdminMCDto.Create(obj);
        }

        public void Update(int id, AdminMCUpdateRequest adminMCUpdateRequest)
        {
            var obj = _adminMCRepository.GetById(id)
                ?? throw new NotFoundException(typeof(AdminMC).ToString(), id);

            if (adminMCUpdateRequest.Name != null) obj.Name = adminMCUpdateRequest.Name;
            if (adminMCUpdateRequest.Email != null) obj.Email = adminMCUpdateRequest.Email;
            if (adminMCUpdateRequest.Password != null) obj.Password = adminMCUpdateRequest.Password;

            _adminMCRepository.Update(obj);
        }
    }
}