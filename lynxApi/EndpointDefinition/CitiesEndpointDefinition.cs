using Application.Entity.Queries;
using Domain.Models;
using lynxApi.Abstractions;
using MediatR;

namespace lynxApi.EndpointDefinition
{
    public class CitiesEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var departure = app.MapGroup("api/city");
            departure.MapGet("/all", GetAllCities);
        }
        private async Task<IResult> GetAllCities(IMediator mediator)
        {
            var getAllCities = new GetAllEntity<City>();
            var Cities = await mediator.Send(getAllCities);
            return TypedResults.Ok(Cities);
        }
    }
}
