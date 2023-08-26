using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IUserRepository
    {
        Task<IResult> GetUserRoleAsync(int id);
        Task<IResult> LoginUserAsync(string login, string password);
        Task<IResult> AddUserAsync(User user);
        Task<IResult> GetAllUsers();
    }
}
