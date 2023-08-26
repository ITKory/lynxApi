using Application.Abstractions;
using Application.Service;
using Application.Users.Queries;
using Microsoft.Extensions.Configuration;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected NeondbContext _context;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _config;

        public UserRepository(IAccountService accountService, IConfiguration configuration, NeondbContext context)
        {
            _context = context;
            _accountService = accountService;
            _config = configuration;
        }

        public async Task<IResult> GetAllUsers()
        {
            return Results.Ok(
                await _context.Users
                .Include(u => u.Profile)
                .Include(u => u.Roles)
                .ToListAsync()
                );
        }
        public async Task<IResult> GetUserRoleAsync(int id)
        {
            var userRoles = _context.Roles
                .Where(r => r.UserId == id);

            var tags = _context.Tags
                .Where(t => userRoles.Where(ur => ur.TagId == t.Id).Any())
                .Select(t => t.Title);

            return Results.Ok(tags);
        }
        public async Task<IResult> LoginUserAsync(string login, string password)
        {
            var user = await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Login.ToLower() == login.ToLower());

            if (user is null)
                return Results.BadRequest();

            if (_accountService.VerifyPassword(user.Password, password.ToLower().Trim()) == false)
                return Results.Unauthorized();

            var userRoles = _context.Roles.Where(r => r.UserId == user.Id);
            var tags = _context.Tags.Where(t => userRoles.Where(ur => ur.TagId == t.Id).Any());

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, login),
            };

            foreach (var tag in tags)
            {
                claims.Add(new Claim(ClaimTypes.Role, tag.Title));
            }

            var jwt = new JwtSecurityToken(
                issuer: _config.GetSection("Ayth:Issuer").Value,
                audience: _config.GetSection("Ayth:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(2)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Ayth:Key").Value)), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new LoggedInUserModel
            {
                Token = encodedJwt,
                User = user,
                Roles = tags
                .Select(t => t.Title)
                .ToArray()
            };

            return Results.Ok(response);
        }

        public async Task<IResult> AddUserAsync(User user)
        {
            user.Login = user.Login
                .Trim().
                ToLower();
            user.Password = user.Password
                .Trim()
                .ToLower();
            user.Email = user.Email
                .Trim()
                .ToLower();

            var registeredUserLogin = await _context.Users.FirstOrDefaultAsync(u => u.Login == user.Login);
            if (registeredUserLogin != null)
                return Results.BadRequest("Login already exist");
            var registeredUserEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (registeredUserEmail != null)
                return Results.BadRequest("Email already exist");
            var registeredUserPhone = await _context.Profiles.FirstOrDefaultAsync(p => p.Phone == user.Profile.Phone);
            if (registeredUserPhone != null)
                return Results.BadRequest("Phone already exist");
 

            var password = _accountService.HashPassword(user.Password);
            var tagId = _context.Tags.FirstOrDefault(t => t.Title.Equals("seeker"))!.Id;

            var newUser = new User()
            {
                Profile = user.Profile,
                Password = password,
                Login = user.Login.ToLower(),
                Email = user.Email,
                Roles = new List<Role>() { new Role() { TagId = tagId } }
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Results.Ok(newUser);
        }
    }
}
