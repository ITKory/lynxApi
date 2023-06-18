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
    public class DeleteProfileHandler : IRequestHandler<DeleteProfile, Profile>
    {
        private readonly IGenericRepository<Profile> _genericRepository;

        public DeleteProfileHandler(IGenericRepository<Profile> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<Profile> Handle(DeleteProfile request, CancellationToken cancellationToken)
        {
          return await _genericRepository.Remove(request.profileId);
        }
    }
}
