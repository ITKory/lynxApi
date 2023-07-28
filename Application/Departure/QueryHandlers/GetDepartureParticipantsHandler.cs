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
    public class GetDepartureParticipantsHandler : IRequestHandler<GetDepartureParticipants, IResult>
    {
        IDepartureRepository _departureRepository;
        public GetDepartureParticipantsHandler(IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public async Task<IResult> Handle(GetDepartureParticipants request, CancellationToken cancellationToken)
        {
            return   _departureRepository.GetDepartureParticipants(request.DepartureId);
        }
    }
}
