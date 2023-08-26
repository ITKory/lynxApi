using Application.Departure.Command;
using Application.Departure.Queries;
using Application.Entity.Queries;
using Application.Profiles.Queries;
using Domain.Models;
using lynxApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace lynxApi.EndpointDefinition;

public class DepartureEndpointDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var departure = app.MapGroup("api/departure");

        departure.MapGet("/all", GetAllDeparture);
        departure.MapGet("/get", GetDeparture);
        departure.MapPost("/registration", RegistrationOnDeparture);
        departure.MapGet("/participants", GetDepartureParticipants);
        departure.MapGet("/locations", GetLocations);
        departure.MapPut("/update", UpdateDeparture);
        departure.MapPost("/add", CreateDeparture);
        departure.MapDelete("/remove", RemoveDeparture);
    }
    private async Task<IResult> GetAllDeparture(IMediator mediator)
    {
        var getAllDepartures = new GetAllDeparture();
        return await mediator.Send(getAllDepartures);

    }
    private async Task<IResult> GetDeparture(IMediator mediator, int departureId)
    {
        var getDeparture = new GetFullDepartureInfo() { DepartureId = departureId };
        return await mediator.Send(getDeparture);
    }
    private async Task<IResult> GetLocations(IMediator mediator)
    {
        var getDepartureLocation = new GetDepartureLocation();
        return await mediator.Send(getDepartureLocation);
    }
    private async Task<IResult> GetDepartureParticipants(IMediator mediator, int departureId)
    {
        var getDeparture = new GetDepartureParticipants() { DepartureId = departureId };
        return await mediator.Send(getDeparture);
    }

  
    private async Task<IResult> RegistrationOnDeparture(IMediator mediator, SeekerRegistrationRequestModel registrationRequestModel)
    {
      var registration = new RegistrationOnDepartureCommand() { 
          SearchDepartureId = registrationRequestModel.DepartureId, 
          UserId = registrationRequestModel.UserId,
          StartTime = registrationRequestModel.StartTime
      };
        return await mediator.Send(registration);
    }    
    private async Task<IResult> UpdateDeparture(IMediator mediator, ListItemModel item)
    {
        var closeDeparture = new UpdateDepartureCommand() { Item = item };
        return await mediator.Send(closeDeparture);
    }

    private async Task<IResult> CreateDeparture(IMediator mediator, SearchDeparture searchDeparture)
    {
        var createDeparture = new AddDepartureCommand() { SearchDeparture = searchDeparture };
        return await mediator.Send(createDeparture);
    } 
    private async Task<IResult> RemoveDeparture(IMediator mediator, int departureId)
    {
        var removeDeparture = new RemoveDepartureCommand() { departureId = departureId };
        return await mediator.Send(removeDeparture);
    }

}
