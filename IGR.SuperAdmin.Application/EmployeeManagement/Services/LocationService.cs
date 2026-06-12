using IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs;
using IGR.SuperAdmin.Application.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repository;

        public LocationService(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<LocationResponseDto> GetByPincode(string pincode)
        {
            return await _repository.GetByPincode(pincode);
        }
    }
}
