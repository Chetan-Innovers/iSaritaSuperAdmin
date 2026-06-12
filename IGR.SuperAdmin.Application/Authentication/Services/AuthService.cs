using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.Authentication.Services
{
    internal class AuthService
    {
        public string Token { get; set; }

        public DateTime Expiry { get; set; }
    }
}
