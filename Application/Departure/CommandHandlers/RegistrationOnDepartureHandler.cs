using Application.Abstractions;
using Application.Departure.Command;
using Application.Profiles.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Departure.CommandHandlers
{
    public class RegistrationOnDepartureHandler : IRequestHandler<RegistrationOnDepartureCommand, IResult>
    {
        private  IDepartureRepository _departureRepository;
        public RegistrationOnDepartureHandler( IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public async Task<IResult> Handle(RegistrationOnDepartureCommand request, CancellationToken cancellationToken)
        {
          return  _departureRepository.RegistrationOnDeparture(request.UserId, request.DepartureId);
        }
    }
}
