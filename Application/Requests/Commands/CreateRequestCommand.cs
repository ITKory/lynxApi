using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Commands
{
    public class CreateRequestCommand:IRequest<IResult>
    {
        public SearchRequest SearchRequest { get; set; }
    }
}
