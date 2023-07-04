using Application.Abstractions;
using Application.Entity.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entity.CommandHandlers
{
    public class UpdateEntityHandler<TEntity> : IRequestHandler<UpdateEntity<TEntity>, TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public UpdateEntityHandler(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<TEntity> Handle(UpdateEntity<TEntity> request, CancellationToken cancellationToken)
        {
             return await _genericRepository.UpdateAsync(request.Entity);
        }

    
    }
}
