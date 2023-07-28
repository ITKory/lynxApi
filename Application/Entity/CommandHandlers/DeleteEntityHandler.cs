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
    public class DeleteEntityHandler<TEntity> : IRequestHandler<DeleteEntity<TEntity>, TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public async Task<TEntity> Handle(DeleteEntity<TEntity> request, CancellationToken cancellationToken)
        {
            return await _genericRepository.Remove(request.Id);
        }
    }
}
