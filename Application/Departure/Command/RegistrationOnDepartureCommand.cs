using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Departure.Command
{
    public class RegistrationOnDepartureCommand : IRequest<IResult>
    {
        public int UserId { get; set; }
        public int DepartureId { get; set; }
    }
}
