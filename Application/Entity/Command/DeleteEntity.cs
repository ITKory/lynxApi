using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entity.Command
{
    public class DeleteEntity<TEntity> : IRequest<TEntity> where TEntity : class
    {
        public int Id { get; set; }
    }
}
