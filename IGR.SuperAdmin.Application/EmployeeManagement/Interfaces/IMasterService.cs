using IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Interfaces
{
    public interface IMasterService
    {
        Task<List<DropdownDto>> GetDig();
        Task<List<DropdownDto>> GetJdr(int digCode);
        Task<List<DropdownDto>> GetSro(int digCode, int jdrCode);
        Task<List<DropdownDto>> GetRoles();
        Task<List<DropdownDto>> GetSecurityQuestions();
    }
}
