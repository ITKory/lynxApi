﻿using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Departure.Queries
{
    public class GetDepartureParticipants : IRequest<IResult>
    {
        public int DepartureId { get; set; }
    }
}