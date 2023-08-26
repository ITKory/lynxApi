using Application.Abstractions;
using Application.Profiles.Commands;
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
using Application.Entity.Command;
using Application.Entity.CommandHandlers;
using Application.Service;
using Application.Users.Command;
using Application.Users.CommandHandlers;
using Application.Departure.Queries;
using Application.Departure.QueryHandlers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;

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
                        ValidIssuer = builder.Configuration["Ayth:Issuer"] ,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Ayth:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Ayth:Key"])),
                    ValidateIssuerSigningKey = true,
                    };
                });
            

            builder.Services.AddDbContext<NeondbContext>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            builder.Services.AddTransient<IGenericRepository<Profile>, GenericRepository<Profile>>();
            builder.Services.AddTransient<IGenericRepository<SearchDeparture>, GenericRepository<SearchDeparture>>();
            builder.Services.AddTransient<IGenericRepository<City>, GenericRepository<City>>();

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IDepartureRepository, DepartureRepository>();
            builder.Services.AddTransient<ISeekerRegistrationRepository, SeekerRegistrationRepository>();
            builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();
            builder.Services.AddTransient<ICrewRepository, CrewRepository>();
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<IRequestRepository, RequestRepository>();

            builder.Services.AddTransient<IRequestHandler<GetAllEntity<SearchDeparture>, ICollection<SearchDeparture>>,GetAllEntityHandler<SearchDeparture>>();
            builder.Services.AddTransient<IRequestHandler<GetAllEntity<City>, ICollection<City>>,GetAllEntityHandler<City>>();
            builder.Services.AddTransient<IRequestHandler<GetAllEntity<Profile>, ICollection<Profile>>,GetAllEntityHandler<Profile>>();
            builder.Services.AddTransient<IRequestHandler<Get<Profile>, Profile>,GetQueryHandler<Profile>>();
            builder.Services.AddTransient<IRequestHandler<Get<User>, User>,GetQueryHandler<User>>();
            builder.Services.AddTransient<IRequestHandler<GetAllEntity<User>, ICollection<User>>,GetAllEntityHandler<User>>();

           builder.Services.AddTransient<IRequestHandler<CreateEntity<Profile>,Profile>, CreateEntityHandler<Profile>>();
           builder.Services.AddTransient<IRequestHandler<CreateEntity<User>,User>, CreateEntityHandler<User>>();
           builder.Services.AddTransient<IRequestHandler<CreateUser,IResult>, CreateUserHandler>();
           builder.Services.AddTransient<IRequestHandler<GetAllDeparture,IResult>, GetAllDepartureHandler>();
           


            builder.Services.AddMediatR(cfg=> cfg.RegisterServicesFromAssemblies(typeof(GetAllEntity<>).Assembly)); 
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllEntityHandler<SearchDeparture>>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllEntityHandler<City>>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllEntityHandler<Profile>>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllEntityHandler<User>>());

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateEntityHandler<User>>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateEntityHandler<Profile>>());


    
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
