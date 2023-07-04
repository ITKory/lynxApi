using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected NeondbContext _context;

        public UserRepository(NeondbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Role>> GetUserRole(int id)
        {
            return await _context.Users.Where(u => u.Id == id)
                .Select(u => u.Roles)
                .FirstOrDefaultAsync();
        }
    }
}
