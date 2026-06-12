using IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs;
using IGR.SuperAdmin.Application.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmployeeExistsResponseDto> CheckUsernameAsync(string username)
        {
            bool exists = await _repository.EmployeeExistsAsync(username);

            var response = new EmployeeExistsResponseDto
            {
                username = username,
                Exists = exists
            };

            if (exists)
            {
                var candidates = new List<string>
                {
                    $"{username}@123",
                    $"{username}#123",
                    $"{username}_{DateTime.Now.Year}",
                    $"{username}.{new Random().Next(100, 999)}",
                    $"{username}{new Random().Next(1000, 9999)}",
                    $"{username}_01",
                    $"{username}@01",
                    $"{username}#{DateTime.Now.Year}"
                };

                foreach (var candidate in candidates)
                {
                    bool candidateExists =
                        await _repository.EmployeeExistsAsync(candidate);

                    if (!candidateExists)
                    {
                        response.Suggestions.Add(candidate);
                    }

                    if (response.Suggestions.Count == 5)
                        break;
                }
            }

            return response;
        }

        public async Task<string> Register(EmployeeRegisterDto dto)
        {
            return await _repository.Register(dto);
        }

        public async Task<List<EmployeeDto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<EmployeeDto?> GetById(long id)
        {
            return await _repository.GetById(id);
        }

        public async Task<string> Update(long id, EmployeeRegisterDto dto)
        {
            return await _repository.Update(id, dto);
        }

        public async Task<string> Delete(long id)
        {
            return await _repository.Delete(id);
        }
        public Task<bool> EmployeeExistsAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
