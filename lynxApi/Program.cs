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

app.RegisterEndpointDefinitions();
app.UseAuthentication();
app.UseAuthorization();


app.Run();

 