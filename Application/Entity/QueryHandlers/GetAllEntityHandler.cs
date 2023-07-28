using Application.Abstractions;
using Application.Entity.Queries;
using Application.Profiles.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entity.QueryHandlers
{
    public class GetAllEntityHandler<TEntity> : IRequestHandler<GetAllEntity<TEntity>, ICollection<TEntity>>  where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;
        public GetAllEntityHandler(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<ICollection<TEntity>> Handle(GetAllEntity<TEntity> request, CancellationToken cancellationToken)
        {
            var temp = await _genericRepository.GetAllAsync();
            return temp;
        }
    
    }
}
