using Application.Abstractions;
using Application.Entity.Command;
using Application.Entity.Queries;
using Application.Entity.QueryHandlers;
using Application.Profiles.Commands;
using Application.Profiles.Queries;
using Application.Users.Command;
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
            profile.MapGet("/get",  GetProfileById);
        }
        private async Task<IResult> GetAllProfiles(IMediator mediator )
        {

            var getAllProfiles = new GetAllEntity<Profile>();
            var profiles =   await mediator.Send(getAllProfiles);
            return Results.Ok(profiles);
                    
        }
        private async Task<IResult> GetProfileById(IMediator mediator, int userId)
        {
            var getUser = new Get<User>() { Predicate = u => u.Id == userId   };
            var user = await mediator.Send(getUser);

            var getProfile = new Get<Profile> { Predicate = p => p.Id == user.ProfileId };
            var profile = await mediator.Send(getProfile);

            return Results.Ok(profile);

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
            var newProfile = new CreateProfile() { NewProfile = profile };
            return await mediator.Send(newProfile);
            
        }
    }
}
