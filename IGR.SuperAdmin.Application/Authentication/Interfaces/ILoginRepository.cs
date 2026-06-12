using IGR.SuperAdmin.Application.EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.Authentication.Interfaces
{
    public interface ILoginRepository
    {
        EmployeeMaster? GetEmployeeByUsername(string username);
        string? GetRoleNameById(int roleId);
        void UpdateEmployee(EmployeeMaster employee);
    }
}
