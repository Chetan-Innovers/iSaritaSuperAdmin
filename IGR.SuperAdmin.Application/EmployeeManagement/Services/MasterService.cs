using IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs;
using IGR.SuperAdmin.Application.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Services
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _repository;

        public MasterService(IMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DropdownDto>> GetDig()
        {
            return await _repository.GetDig();
        }

        public async Task<List<DropdownDto>> GetJdr(int digCode)
        {
            return await _repository.GetJdr(digCode);
        }

        public async Task<List<DropdownDto>> GetSro(int digCode, int jdrCode)
        {
            return await _repository.GetSro(digCode, jdrCode);
        }

        public async Task<List<DropdownDto>> GetRoles()
        {
            return await _repository.GetRoles();
        }

        public async Task<List<DropdownDto>> GetSecurityQuestions()
        {
            return await _repository.GetSecurityQuestions();
        }
    }
}
