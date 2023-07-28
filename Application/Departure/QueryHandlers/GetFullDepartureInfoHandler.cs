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
    public class GetFullDepartureInfoHandler : IRequestHandler<GetFullDepartureInfo, IResult>
    {
        IDepartureRepository _departureRepository;
        public GetFullDepartureInfoHandler(IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public async Task<IResult> Handle(GetFullDepartureInfo request, CancellationToken cancellationToken)
        {
            return await _departureRepository.GetDeparture(request.DepartureId);
        }
    }
}
