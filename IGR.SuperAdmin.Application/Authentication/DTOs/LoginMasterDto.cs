using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.Authentication.DTOs
{
    public class LoginMasterDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public LoginDataDto Data { get; set; }
    }
    public class LoginDataDto
    {
        public UserDto User { get; set; }
        public AuthDto Auth { get; set; }
        public RoleDto Role { get; set; }
    }
    public class UserDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class AuthDto
    {
        public string AccessToken { get; set; }
    }
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
