using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Requests.Queries
{
    public class GetFullRequestInfo:IRequest<IResult>
    {
        public int RequestId { get; set; }
    }
}
