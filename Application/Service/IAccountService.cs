using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public interface IAccountService
    {
        public string GetMyRole();
        public string HashPassword(string password);
        bool VerifyPassword(string passwordHash, string password);
    }
}
