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
    public class GetRequestsStatQueryHandler : IRequestHandler<GetRequestsStat, IResult>
    {
        private IStatisticRepository _statisticRepository;
        public GetRequestsStatQueryHandler(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        public async Task<IResult> Handle(GetRequestsStat request, CancellationToken cancellationToken)
        {
            return await _statisticRepository.GetRequestStat();
        }
    }
}
