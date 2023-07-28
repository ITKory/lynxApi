using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class AccountService:IAccountService
    {
        IHttpContextAccessor _httpContextAccessor;

        public AccountService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string HashPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public bool VerifyPassword(string passwordHash, string password)
            => BCrypt.Net.BCrypt.Verify(password, passwordHash);

        public string GetMyRole()
        {
            var result = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (_httpContextAccessor.HttpContext != null)
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            return result;
        }
    }
}
