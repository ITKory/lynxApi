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
    public class UpdateProfileHandler : IRequestHandler<UpdateProfile, Profile>
    {
        private readonly IGenericRepository<Profile> _genericRepository;

        public UpdateProfileHandler(IGenericRepository<Profile> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Profile> Handle(UpdateProfile request, CancellationToken cancellationToken)
        {
            _genericRepository.Update(request.ChangeableProfile);
            return request.ChangeableProfile;
        }
    }
}
