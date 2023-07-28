using Application.Abstractions;
using Application.Service;
using Application.Users.Command;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Users.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser, IResult>
    {
        private readonly  IUserRepository _userRepository;
        
        public CreateUserHandler( IUserRepository userRepository  )
        {
            _userRepository = userRepository;
        }

        public async Task<IResult> Handle(CreateUser request, CancellationToken cancellationToken)
        {
           return await _userRepository.AddUser(request.NewUser);
        }
    }
}
