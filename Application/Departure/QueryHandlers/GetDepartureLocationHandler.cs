using Application.Abstractions;
using Application.Departure.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Departure.QueryHandlers
{
    public class GetDepartureLocationHandler : IRequestHandler<GetDepartureLocation, IResult>
    {
        IDepartureRepository _departureRepository;

        public GetDepartureLocationHandler(IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public async Task<IResult> Handle(GetDepartureLocation request, CancellationToken cancellationToken)
        {
            return await _departureRepository.GetDepartureLocationAsync();
        }
    }
}
