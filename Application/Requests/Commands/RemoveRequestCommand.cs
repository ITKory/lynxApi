using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Commands
{
    public class RemoveRequestCommand : IRequest<IResult>
    {
        public int RequestId { get;set; }
    }
}
