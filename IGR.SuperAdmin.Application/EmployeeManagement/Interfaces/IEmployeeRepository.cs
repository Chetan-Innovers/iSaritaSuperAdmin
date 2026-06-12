using IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<bool> EmployeeExistsAsync(string username);
        Task<string> Register(EmployeeRegisterDto dto);
        Task<List<EmployeeDto>> GetAll();
        Task<EmployeeDto?> GetById(long id);
        Task<string> Update(long id, EmployeeRegisterDto dto);
        Task<string> Delete(long id);
    }

}
