using Application.Abstractions;
using Application.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Requests.CommandHandlers
{
    public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, IResult>
    {
        IRequestRepository _requestRepository;
        public CreateRequestCommandHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<IResult> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
          return await  _requestRepository.CreateRequestAsync(request.SearchRequest);
        }
    }
}
