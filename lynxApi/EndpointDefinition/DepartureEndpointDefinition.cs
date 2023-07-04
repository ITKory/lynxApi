using Application.Entity.Queries;
using Application.Profiles.Queries;
using Domain.Models;
using lynxApi.Abstractions;
using MediatR;

namespace lynxApi.EndpointDefinition;
    
public class DepartureEndpointDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var departure = app.MapGroup("api/departure");

        departure.MapGet("/all", GetAllDeparture);
    }
    private async Task<IResult> GetAllDeparture( IMediator mediator)
    {
        var getAllDepartures = new GetAllEntity<SearchDeparture>();
      var departures =    await mediator.Send(getAllDepartures);
        return TypedResults.Ok(departures);
    }

}
