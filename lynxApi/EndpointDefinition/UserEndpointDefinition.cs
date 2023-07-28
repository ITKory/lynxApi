using Application.Entity.Command;
using Application.Entity.Queries;
using Application.Users.Command;
using Application.Users.Queries;
using Domain.Models;
using lynxApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace lynxApi.EndpointDefinition
{
    public class UserEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var departure = app.MapGroup("api/user");

            departure.MapPost("/add", AddUser);
            departure.MapGet("/all", GetAllUsers);
            departure.MapPost("/login", LoginUser);
            departure.MapPost("/user-ping", AuthorizePing);
            departure.MapPost("/admin-ping", AdminPing);
            departure.MapPost("/ping",  Ping);
        }

        [Authorize]
        private async Task<IResult> GetAllUsers(IMediator mediator)
        {
            var getAllUsers = new GetAllEntity<User>();
            var users = await mediator.Send(getAllUsers);
            return TypedResults.Ok(users);
        }
        private async Task<IResult> AddUser(IMediator mediator, User user)
        {
            var newUser = new CreateUser {  NewUser = user };
            return await mediator.Send(newUser);
 
       
        }

        private async Task<IResult> LoginUser(IMediator mediator, LoginModel loginModel)  
        {

            var loginUser = new LoginUser() { Login = loginModel.Login, Password = loginModel.Password };
            return await mediator.Send(loginUser);
    
        }

        public async Task Ping(HttpContext context)
        {
            await context.Response
                .WriteAsJsonAsync("pong");
        }

 
        [Authorize]
        public async Task AuthorizePing(HttpContext context)
        {
            await context.Response
                .WriteAsJsonAsync("authorize pong");
        }

     
        [Authorize(Roles = "admin")]
        public async Task AdminPing(HttpContext context)
        {
            await context.Response
                .WriteAsJsonAsync("admin pong");
        }

    }
}
