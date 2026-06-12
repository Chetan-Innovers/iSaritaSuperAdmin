using IGR.SuperAdmin.Application.Authentication.DTOs;
using IGR.SuperAdmin.Application.Authentication.Interfaces;
using IGR.SuperAdmin.Application.Authentication.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.Authentication.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repository;
        private readonly IConfiguration _configuration;

        public LoginService(ILoginRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public LoginMasterDto Login(LoginRequest request)
        {
            var employee = _repository.GetEmployeeByUsername(request.Username);

            if (employee == null)
            {
                return new LoginMasterDto
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            var roleName = _repository.GetRoleNameById(employee.RoleId);

            if (employee.IsLocked)
            {
                return new LoginMasterDto
                {
                    Success = false,
                    Message = "Account is locked"
                };
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                employee.PasswordHash);

            if (!isPasswordValid)
            {
                employee.LoginAttemptCount++;

                if (employee.LoginAttemptCount >= 5)
                {
                    employee.IsLocked = true;
                    employee.LockedAt = DateTime.UtcNow;
                }

                _repository.UpdateEmployee(employee);

                return new LoginMasterDto
                {
                    Success = false,
                    Message = "Invalid password"
                };
            }

            employee.LoginAttemptCount = 0;
            employee.LastLoginAt = DateTime.UtcNow;

            _repository.UpdateEmployee(employee);

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("EmpId", employee.EmpId.ToString()),
            new Claim("Username", employee.Username),
            new Claim("RoleId", employee.RoleId.ToString()),
            new Claim("FirstName", employee.FirstName),

            new Claim(JwtRegisteredClaimNames.Sub, employee.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),

                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginMasterDto
            {
                Success = true,
                Message = "Login successful",

                Data = new LoginDataDto
                {
                    User = new UserDto
                    {
                        Id = employee.EmpId,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName
                    },

                    Auth = new AuthDto
                    {
                        AccessToken = tokenHandler.WriteToken(token)
                    },

                    Role = new RoleDto
                    {
                        Id = employee.RoleId,
                        Name = roleName
                    }
                }
            };
        }
    }
}
