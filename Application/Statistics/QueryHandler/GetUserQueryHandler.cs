using Application.Abstractions;
using Application.Statistics.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Statistics.QueryHandler
{
    public class GetUserQueryHandler : IRequestHandler<GetUserStat, IResult>
    {
        private readonly IStatisticRepository _statisticRepository;
        public GetUserQueryHandler(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }
        public async Task<IResult> Handle(GetUserStat request, CancellationToken cancellationToken)
        {
          return await _statisticRepository.GetUserStat(request.Id);
        }
    }
}
