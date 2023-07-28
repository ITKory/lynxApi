using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Statistics.Queries
{
    public class GetUserStat : IRequest<IResult>
    {
        public int Id { get; set; }
    }
}
