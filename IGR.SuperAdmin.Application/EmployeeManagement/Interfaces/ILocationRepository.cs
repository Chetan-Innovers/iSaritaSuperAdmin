using IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Interfaces
{
    public interface ILocationRepository
    {
        Task<LocationResponseDto> GetByPincode(string pincode);
    }
}
