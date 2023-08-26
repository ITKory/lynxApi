using Application.Abstractions;
using Application.Profiles.Commands;
using DataAccess;
using Domain.Models;
using MediatR;
using Application.Profiles.Queries;
using lynxApi.Extensions;



var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory())
    .UseUrls("http://localhost:5008", "http://192.168.1.95:5008");

var app = builder.Build();
app.RegisterEndpointDefinitions();
app.UseAuthentication();
app.UseAuthorization();


app.Run();

 