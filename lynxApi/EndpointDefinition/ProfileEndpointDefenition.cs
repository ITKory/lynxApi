using Application.Abstractions;
using Application.Profiles.Commands;
using Application.Profiles.Queries;
using Domain.Models;
using lynxApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lynxApi.EndpointDefinition
{
    public class ProfileEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var profile = app.MapGroup("api/profile");

            profile.MapGet("/all",GetAllProfiles);
            profile.MapPost("/add", AddProfile);
            profile.MapPost("/update", UpdateProfile);
            profile.MapDelete("/del/{id}",  DeleteProfile);
        }
        private async Task<IResult> GetAllProfiles(IMediator mediator )
        {
            var profiles = new GetAllProfiles();
            await mediator.Send(profiles);
            return TypedResults.Ok(profiles);
        }
          private async Task<IResult> DeleteProfile(IMediator mediator, int id )
            {
            var removableProfile = new DeleteProfile() {  profileId = id };
            var  removedProfile= await mediator.Send(removableProfile);
            return Results.Ok(removedProfile);
        } 
        private async Task<IResult> UpdateProfile(IMediator mediator, Profile profile)
        {
            var updatableProfile = new UpdateProfile() { ChangeableProfile = profile };
            var updatedProfile = await mediator.Send(updatableProfile);
            return Results.Ok(updatedProfile);
        } 
        private async Task<IResult> AddProfile(IMediator mediator, Profile profile) 
        {
            /*var newProfile = new CreateProfile() { NewProfile = profile };
            var createdProfile = await mediator.Send(newProfile);
            return Results.Ok(createdProfile);*/

            var newProfile = new CreateEntity<Profile>() { Entity = profile };
            var createdProfile = await mediator.Send(newProfile);
            return TypedResults.Ok(createdProfile);
        }
    }
}
