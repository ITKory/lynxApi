using Application.Abstractions;
using Application.Service;
using Application.Users.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.QueryHandlers
{
    public class LoginUserHandler : IRequestHandler<LoginUser, IResult>
    {
        private readonly IUserRepository _userRepository;
      

        public LoginUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
          
        }

        public async Task<IResult> Handle(LoginUser request, CancellationToken cancellationToken)
        {
           return await _userRepository.LoginUser(request.Login, request.Password);
        }
    }
}
