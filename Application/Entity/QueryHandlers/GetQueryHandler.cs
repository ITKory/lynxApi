using Application.Abstractions;
using Application.Entity.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entity.QueryHandlers
{
    public class GetQueryHandler<TEntity> : IRequestHandler<Get<TEntity>, TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GetQueryHandler(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<TEntity> Handle(Get<TEntity> request, CancellationToken cancellationToken)
        {
         return  _genericRepository.Get(request.Predicate).First();
        }
    }
}
