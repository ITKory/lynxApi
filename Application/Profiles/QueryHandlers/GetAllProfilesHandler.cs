using Application.Abstractions;
using Application.Profiles.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles.QueryHandlers
{
    public class GetAllProfilesHandler : IRequestHandler<GetAllProfiles, ICollection<Profile>>
    {
        private readonly IGenericRepository<Profile> _genericRepository;
        public GetAllProfilesHandler(IGenericRepository<Profile> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<ICollection<Profile>> Handle(GetAllProfiles request, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetAllAsync();
             
        }
    }
}
