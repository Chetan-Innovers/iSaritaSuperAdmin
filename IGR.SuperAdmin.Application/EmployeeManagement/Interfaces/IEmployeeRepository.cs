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
    }
}
