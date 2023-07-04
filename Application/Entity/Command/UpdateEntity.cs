using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entity.Command
{
    public class UpdateEntity<TEntity> : IRequest<TEntity> where TEntity : class
    {
        public TEntity Entity { get; set; }
    }
}
