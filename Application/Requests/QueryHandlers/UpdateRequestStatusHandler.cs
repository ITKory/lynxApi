using Application.Abstractions;
using Application.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.QueryHandlers
{
    internal class UpdateRequestStatusHandler : IRequestHandler<UpdateRequestStatus, IResult>
    {
        IRequestRepository _requestRepository;
        public UpdateRequestStatusHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<IResult> Handle(UpdateRequestStatus request, CancellationToken cancellationToken)
        {
          return await  _requestRepository.UpdateStatusAsync(request.request);
        }
    }
}
