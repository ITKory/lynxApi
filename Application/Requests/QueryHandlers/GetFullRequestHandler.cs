using Application.Abstractions;
using Application.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Requests.QueryHandlers
{
    public class GetFullRequestHandler : IRequestHandler<GetFullRequestInfo, IResult>
    {
        IRequestRepository _requestRepository;

        public GetFullRequestHandler( IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<IResult> Handle(GetFullRequestInfo request, CancellationToken cancellationToken)
        {
            return await _requestRepository.GetRequestAsync(request.RequestId);
        }
    }
}
