using Application.Abstractions;
using Application.Departure.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Departure.CommandHandlers
{
    public class AddDepartureCommandHandler : IRequestHandler<AddDepartureCommand, IResult>
    {
        IDepartureRepository _departureRepository;

        public AddDepartureCommandHandler(IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public async Task<IResult> Handle(AddDepartureCommand request, CancellationToken cancellationToken)
        {
            return await _departureRepository.CreateDeparture(request.SearchDeparture);
        }
    }
}
