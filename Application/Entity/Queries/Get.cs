using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entity.Queries
{
    public class Get<TEntity> : IRequest<TEntity> where TEntity : class
    {
        public Func<TEntity, bool> Predicate { get; set; }
    }
}
