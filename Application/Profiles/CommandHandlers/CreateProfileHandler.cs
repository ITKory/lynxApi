using Application.Abstractions;
using Application.Profiles.Commands;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles.CommandHandlers
{
    public class CreateProfileHandler : IRequestHandler<CreateProfile, IResult>
    {
        private readonly IGenericRepository<Profile> _genericRepository;

        public CreateProfileHandler(IGenericRepository<Profile> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IResult> Handle(CreateProfile request, CancellationToken cancellationToken)
        {
         
            var profilePhone = _genericRepository
                .Get((p) => { return p.Phone == request.NewProfile.Phone; })
                .FirstOrDefault()?.Phone ;

            if (profilePhone != null)
                return Results.BadRequest();
          
            var NewProfile = await _genericRepository
                .CreateAsync(request.NewProfile);

            return Results.Ok(NewProfile);
        }
    }
}
