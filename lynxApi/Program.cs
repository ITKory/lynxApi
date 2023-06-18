using Application.Abstractions;
using Application.Profiles.Commands;
using DataAccess;
using Domain.Models;
using MediatR;
using Application.Profiles.Queries;
using lynxApi.Extensions;



var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.RegisterEndpointDefinitions();
 

//User
/*app.MapGet("/user/all", async (IMediator mediator, IGenericRepository<User> user) =>
{
    return await user.GetAllAsync();
});
app.MapPost("/user/add", async (IMediator mediator, IGenericRepository<User> repository, User user) => {

    if (user is null) return new();
    return await repository.CreateAsync(user);
});
app.MapPost("/user/update", (IMediator mediator, IGenericRepository<User> repository, User user) =>
{
    repository.Update(user);
    var student = repository.FindById(user.Id);
    return student;

});
app.MapDelete("/user/del/{id}", (IMediator mediator, int id, IGenericRepository<User> repository) =>
{
    var user = repository.FindById(id);
    if (user is not null)
        repository.Remove(user);
    return StatusCodes.Status200OK;

});*/
app.Run();

 