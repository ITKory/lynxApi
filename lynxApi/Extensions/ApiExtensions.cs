using Application.Abstractions;
using Application.Profiles.Commands;
using DataAccess.Auth;
using DataAccess.Repositories;
using DataAccess;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MediatR;
using MediatR.Pipeline;
using lynxApi.Abstractions;
using Application.Profiles.Queries;
using Application.Profiles.CommandHandlers;
using Application.Profiles.QueryHandlers;
using Application.Entity.Queries;
using Application.Entity.QueryHandlers;
using System.Reflection;

namespace lynxApi.Extensions
{
    public static class ApiExtensions
    {
        public  static void RegisterServices( this WebApplicationBuilder builder)
        {
             
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOption.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOption.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOption.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });

            builder.Services.AddDbContext<NeondbContext>();
            builder.Services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            builder.Services.AddTransient<IGenericRepository<Profile>, GenericRepository<Profile>>();
            builder.Services.AddTransient<IGenericRepository<SearchDeparture>, GenericRepository<SearchDeparture>>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IDepartureRepository, DepartureRepository>();
            builder.Services.AddTransient<ISeekerRegistrationRepository, SeekerRegistrationRepository>();
            builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();
            builder.Services.AddTransient<ICrewRepository, CrewRepository>();

            builder.Services.AddTransient<IRequestHandler<GetAllEntity<SearchDeparture>, ICollection<SearchDeparture>>,GetAllEntityHandler<SearchDeparture>>();
           


            builder.Services.AddMediatR(cfg=> cfg.RegisterServicesFromAssemblies(typeof(GetAllEntity<>).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllEntityHandler<SearchDeparture>>());


    
        }
        public static void RegisterEndpointDefinitions(this WebApplication  app )
        {
            var endpointDefinition = typeof(Program).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();

            foreach (var endpointDef in endpointDefinition)
                endpointDef.RegisterEndpoints(app);
        }
    }
}
