using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetRole : IRequest<ICollection<Role>>
    {
        public int Id { get; set; }
    }
}
