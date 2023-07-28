using Application.Departure.Command;
using Application.Departure.Queries;
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
        departure.MapPost("/fullInfo", GetDeparture);
        departure.MapPost("/registration", RegistrationOnDeparture);
        departure.MapPost("/participants", GetDepartureParticipants);
    }
    private async Task<IResult> GetAllDeparture( IMediator mediator)
    {
        var getAllDepartures = new GetAllDeparture();
        return  await mediator.Send(getAllDepartures);
    
    } 
    private async Task<IResult> GetDeparture( IMediator mediator , int departureId)
    {
        var getDeparture = new GetFullDepartureInfo() { DepartureId = departureId};
        return  await mediator.Send(getDeparture);
    }
    private async Task<IResult> GetDepartureParticipants(IMediator mediator, int departureId)
    {
        var getDeparture = new GetDepartureParticipants() { DepartureId = departureId };
        return await mediator.Send(getDeparture);
    }
    private async Task<IResult> RegistrationOnDeparture( IMediator mediator , int departureId , int userId)
    {
      var registration = new RegistrationOnDepartureCommand() { DepartureId = departureId, UserId = userId };
        return await mediator.Send(registration);
    }

}
