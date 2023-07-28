using Application.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class DepartureRepository : IDepartureRepository
{
    protected NeondbContext _context;

    public DepartureRepository(NeondbContext context)
    {
        _context = context;
    }

    public IResult RegistrationOnDeparture(int userId, int departureId)
    {
        if (_context.Users.Where(u => u.Id == userId).Any() == false)
            return Results.BadRequest();

        if (_context.SearchDepartures.Where(d => d.Id == departureId).Any() == false)
            return Results.BadRequest();

        _context.SeekerRegistrations
            .Add(new SeekerRegistration()
            {
                UserId = userId,
                SearchDepartureId = departureId
            });

        _context.SaveChanges();

        return Results.Ok();

    }

    public IResult GetDepartureParticipants(int departureId)
    {
        List<User> participants = new();
        var users = _context.SeekerRegistrations
       .Where(s => s.SearchDeparture.Id == departureId)
       .Select(s => s.UserId);

        foreach (var user in users)
            participants.Add(_context.Users.Find(user));

        return Results.Ok(participants);

    }
    public async Task<IResult> GetDeparture(int departureId)
    {

        var departure = await _context.SearchDepartures
               .Where(d => d.Id == departureId)
               .Include(d => d.SearchRequest)
               .ThenInclude(s => s.Lost)
               .ThenInclude(l => l.City)
               .Include(s => s.Location)
               .Include(d => d.SearchAdministrator)
               .FirstOrDefaultAsync();
        
        if (departure == null)
            return Results.BadRequest();

        return Results.Ok(departure);
    }
    public async Task<IResult> GetAllDeparture()
    {
        var departures = await _context.SearchDepartures
            .Include(d => d.SearchRequest)
            .ThenInclude(s => s.Lost)
            .ThenInclude(s => s.City)
            .Include(d => d.Location)
            .ToListAsync();

        if (departures == null)
            return Results.BadRequest();

        var departureList = new List<DepartureListItemModel>();

        foreach (var item in departures)
        {
            departureList.Add(new DepartureListItemModel()
            {
                Id = item.Id,
                Title = item.SearchRequest.Lost.Name,
                BDay = item.SearchRequest.Lost.Birthday,
                City = item.SearchRequest.Lost.City.Title,
                IsActive = item.SearchRequest.IsActive,
                IsFound = item.SearchRequest.IsFound

            });
        }

        return Results.Ok(departureList);
    }

}
