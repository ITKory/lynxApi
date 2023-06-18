using Application.Abstractions;
using Application.Profiles.Commands;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles.CommandHandlers
{
    public class CreateProfileHandler : IRequestHandler<CreateProfile, Profile>
    {
        private readonly IGenericRepository<Profile> _genericRepository;

        public CreateProfileHandler(IGenericRepository<Profile> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Profile> Handle(CreateProfile request, CancellationToken cancellationToken)
        {
         var newProfile = new Profile
            {
                Name = request.NewProfile.Name,
                BDay = request.NewProfile.BDay,
                Phone = request.NewProfile.Phone,
                RelativesPhone = request.NewProfile.RelativesPhone,
                Call = request.NewProfile.Call
            };
            return await _genericRepository.CreateAsync(newProfile);
        }
    }
}
