using Application.Abstractions;
using Application.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.CommandHandlers
{
    public class RemoveRequestCommandHandler : IRequestHandler<RemoveRequestCommand, IResult>
    {
        IRequestRepository _requestRepository;

        public RemoveRequestCommandHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<IResult> Handle(RemoveRequestCommand request, CancellationToken cancellationToken)
        {
            return await _requestRepository.RemoveRequestAsync(request.RequestId);
        }
    }
}
