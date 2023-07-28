using Application.Abstractions;
using Application.Entity.Command;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entity.CommandHandlers
{
    public class CreateEntityHandler<TEntity> : IRequestHandler<CreateEntity<TEntity>, TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public CreateEntityHandler(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<TEntity> Handle(CreateEntity<TEntity> request, CancellationToken cancellationToken)
        {
            return await _genericRepository.CreateAsync(request.Entity);

        }
    }

}
