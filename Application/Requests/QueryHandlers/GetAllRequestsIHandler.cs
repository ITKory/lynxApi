using Application.Abstractions;
using Application.Departure.Queries;
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
    public class GetAllRequestsIHandler : IRequestHandler<GetAllRequests, IResult>
    {
        IRequestRepository _requestRepository;
        public GetAllRequestsIHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<IResult> Handle(GetAllRequests request, CancellationToken cancellationToken)
        {
            return await _requestRepository.GetAllRequestsAsync();
        }
    }
}
