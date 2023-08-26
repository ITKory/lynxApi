using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Departure.Command
{
    public class UpdateDepartureCommand : IRequest<IResult>
    {
        public ListItemModel Item { get; set; }
    }
}
