using IGR.SuperAdmin.Application.Authentication.DTOs;
using IGR.SuperAdmin.Application.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.Authentication.Interfaces
{
    public interface ILoginService
    {
        LoginMasterDto Login(LoginRequest request);
    }

}
