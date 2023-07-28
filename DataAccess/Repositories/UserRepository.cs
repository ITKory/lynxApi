using Application.Abstractions;
using Application.Service;
using Application.Users.Queries;
using DataAccess.Auth;
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
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected NeondbContext _context;
        private readonly IAccountService _accountService;

        public UserRepository(IAccountService accountService,NeondbContext context )
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<IResult> GetUserRole(int id)
        {
            var userRoles = _context.Roles
                .Where(r => r.UserId == id);

            var tags =  _context.Tags
                .Where(t => userRoles.Where(ur => ur.TagId == t.Id).Any())
                .Select(t=>t.Title);
          
            return Results.Ok(tags);

        }
        public async Task<IResult> LoginUser( string login , string password)
        {
 

            var user = await _context.Users
                .Include(u=>u.Profile)
                .FirstOrDefaultAsync(u => u.Login == login);

            if(user is null) 
                return Results.BadRequest();
                
            if (_accountService.VerifyPassword(user.Password, password) == false)
                return Results.Unauthorized();


            var userRoles =   _context.Roles.Where(r => r.UserId == user.Id);
            var tags = _context.Tags.Where(t=>userRoles.Where(ur=>ur.TagId ==  t.Id).Any());

            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, login),
                };

            foreach(var tag in tags)
            {
                claims.Add(new Claim(ClaimTypes.Role, tag.Title));
            }
 
                var jwt = new JwtSecurityToken(
                    issuer: AuthOption.ISSUER,
                    audience: AuthOption.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(2)),
                    signingCredentials: new SigningCredentials(AuthOption.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

         
                var response = new LoggedInUserModel
                {
                    Token = encodedJwt,
                    User = user,
                    Roles = tags.Select(t=>t.Title).ToArray()

                };

                return Results.Ok(response);
         }

        public async Task<IResult> AddUser( User user)
        {
            var registeredUserLogin = await _context.Users.FirstOrDefaultAsync(u => u.Login == user.Login);
            var registeredUserPhone = await _context.Profiles.FirstOrDefaultAsync(p => p.Phone == user.Profile.Phone);
           
            if (registeredUserLogin !=  null || registeredUserLogin != null)
                return Results.BadRequest();

            var password = _accountService.HashPassword(user.Password);
            var tagId = _context.Tags.FirstOrDefault(t => t.Title.Equals("seeker"))!.Id;

            var newUser = new User()
            {
                Profile = user.Profile,
                Password = password,
                Login = user.Login,
                Email = user.Email,
                Roles = new List<Role>() { new Role() { TagId = tagId } }
                
            };

        
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Results.Ok(newUser); 
        }
    }
}
