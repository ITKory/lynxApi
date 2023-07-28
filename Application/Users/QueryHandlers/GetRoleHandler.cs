using Application.Abstractions;
using Application.Profiles.Queries;
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
    public class GetRoleHandler : IRequestHandler<GetRole, IResult>
    {
        private readonly IUserRepository _repository;

        public GetRoleHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(GetRole request, CancellationToken cancellationToken)
        {
            return await _repository.GetUserRole(request.Id);
        }
    }
}
