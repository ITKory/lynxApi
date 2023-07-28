using Application.Abstractions;
using Application.Departure.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Departure.QueryHandlers
{
    public class GetAllDepartureHandler : IRequestHandler<GetAllDeparture, IResult>
    {
        IDepartureRepository _departureRepository;
        public GetAllDepartureHandler(IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public async Task<IResult> Handle(GetAllDeparture request, CancellationToken cancellationToken)
        {
            return await _departureRepository.GetAllDeparture();
        }
    }
}
