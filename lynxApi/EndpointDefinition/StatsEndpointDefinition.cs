using Application.Abstractions;
using Application.Departure.Queries;
using Application.Statistics.Queries;
using lynxApi.Abstractions;
using MediatR;

namespace lynxApi.EndpointDefinition
{
    public class StatsEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var stats = app.MapGroup("api/stats");
            stats.MapGet("/user",GetUserStat);
            stats.MapGet("/request",GetRequestsStat);
        }

        private async Task<IResult> GetUserStat(IMediator mediator , int id)
        {
        
            var user = new GetUserStat() { Id = id };
            return await mediator.Send(user);
        }     
        private async Task<IResult> GetRequestsStat(IMediator mediator )
        {
            var requests = new GetRequestsStat()  ;
            return await mediator.Send(requests);
        }

    }
}
