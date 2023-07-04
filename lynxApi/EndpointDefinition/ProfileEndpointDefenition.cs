﻿using Application.Abstractions;
using Application.Entity.Command;
using Application.Entity.Queries;
using Application.Profiles.Commands;
using Application.Profiles.Queries;
using Domain.Models;
using lynxApi.Abstractions;
using MediatR;

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

            var getAllprofiles = new GetAllProfiles();
            var profiles =   await mediator.Send(getAllprofiles);
            return TypedResults.Ok(profiles);

            /*   var profiles = new GetAllEntity<Profile>();
               await mediator.Send(profiles);
               return TypedResults.Ok(profiles);*/
        }
        private async Task<IResult> DeleteProfile(IMediator mediator, int id )
            {
            var removableProfile = new DeleteEntity<Profile>() {  Id = id };
            var  removedProfile= await mediator.Send(removableProfile);
            return Results.Ok(removedProfile);
        } 
        private async Task<IResult> UpdateProfile(IMediator mediator, Profile profile)
        {
            var updatableProfile = new UpdateEntity<Profile>() { Entity = profile };
            var updatedProfile = await mediator.Send(updatableProfile);
            return Results.Ok(updatedProfile);
        } 
        private async Task<IResult> AddProfile(IMediator mediator, Profile profile) 
        {
            var newProfile = new CreateEntity<Profile>() { Entity = profile };
            var createdProfile = await mediator.Send(newProfile);
            return TypedResults.Ok(createdProfile);
        }
    }
}
