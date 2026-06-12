using IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs;
using IGR.SuperAdmin.Application.EmployeeManagement.Interfaces;
using IGR.SuperAdmin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Infrastructure.Repositories.EmployeeManagement
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _context;

        public LocationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LocationResponseDto> GetByPincode(string pincode)
        {
            var pin = await _context.PincodeMasters
                      .Where(x => x.Pincode == pincode)
                      .Select(x => new LocationResponseDto
                      {
                          District = x.DistrictName ?? "",
                          Taluka = x.Taluka ?? "",
                          Village = x.VillageName ?? "",
                          City = x.CityName ?? "",
                          State = x.State ?? "",
                          StateShortName = x.StateShortName ?? ""
                      })
                      .FirstOrDefaultAsync();

            return pin;
        }
    }
}
