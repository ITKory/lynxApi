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


    public async Task<IResult> GetDepartureLocationAsync()
    {
        var departures = await _context.SearchDepartures
             .Include(sd => sd.Location)
             .ToListAsync();
        var locations = departures.Select(sd => $"{sd.Location.Latitude},{sd.Location.Longitude}");

        return Results.Ok(locations);
    }
    public async Task<IResult> CloseDeparture(int departureId)
    {
        var departure = _context.SearchDepartures.First(s => s.Id == departureId);
        if (departure == null)
        {
            return Results.BadRequest();
        }
        departure.IsActive = false;

        await _context.SeekerRegistrations
              .Where(s => s.SearchDeparture.Id == departureId && s.EndAt == s.StartAt)
              .ForEachAsync(s => s.EndAt = TimeOnly.FromDateTime(DateTime.Now));
        await _context.SaveChangesAsync();

        return Results.Ok();
    }


    public async Task<IResult> UpdateStatusAsync(ListItemModel item)
    {

        var departure = _context.SearchDepartures
            .Include(s=>s.SearchRequest)
            .First(s => s.Id == item.Id);

        if (departure == null)
        {
            return Results.BadRequest();
        }
            if(item.IsFound == true)
            {
            departure.SearchRequest.IsActive = false;
            }

            departure.SearchRequest.IsDied = item.IsDied;
            departure.SearchRequest.IsFound = item.IsFound;
            departure.IsActive = item.IsActive;
            _context.SearchDepartures.Update(departure);
            await _context.SaveChangesAsync();
  
        return Results.Ok();

    }



    public async Task<IResult> RemoveDeparture(int departureId)
    {
        var departure = _context.SearchDepartures.First(s => s.Id == departureId);
        if (departure == null)
        {
            return Results.BadRequest();
        }

        _context.SearchDepartures.Remove(departure);
        await _context.SaveChangesAsync();

        return Results.Ok(departure);
    }
    public IResult LeaveDeparture(int userId, int departureId, TimeOnly LeaveAt)
    {
        var sr = _context.SeekerRegistrations.First(s => s.UserId == userId && s.SearchDepartureId == departureId);

        if (sr == null)
        {
            return Results.BadRequest();
        }

        sr.EndAt = LeaveAt;
        _context.SeekerRegistrations.Update(sr);
        return Results.Ok();
    }
    public IResult RegistrationOnDeparture(int userId, int departureId, TimeOnly startAt)
    {
        _context.SeekerRegistrations
            .Add(new SeekerRegistration()
            {
                UserId = userId,
                SearchDepartureId = departureId,
                StartAt = startAt,
                EndAt = startAt
            });

        _context.SaveChanges();

        return Results.Ok();

    }

    public async Task<IResult> CreateDeparture(SearchDeparture searchDeparture)
    {
        try
        {
            _context.SearchDepartures.Add(searchDeparture);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
        await _context.SaveChangesAsync();
        return Results.Ok();
    }
    public IResult GetDepartureParticipants(int departureId)
    {
        var users = _context.SeekerRegistrations
            .Include(s => s.User)
            .ThenInclude(u => u.Profile)
            .Where(s => s.SearchDepartureId == departureId && s.EndAt == s.StartAt)
            .Select(s => s.User)
            .ToList();

        var participants = users.Select(u => new
        {
            id = u.Id,
            name = u.Profile.Name,
            call = u.Profile.Call
        });

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

        var departureList = new List<ListItemModel>();


        foreach (var departure in departures)
        {
            departureList.Add(new()
            {
                IsActive = departure.IsActive,
                IsFound = departure.SearchRequest.IsFound,
                IsDied = departure.SearchRequest.IsDied,
                Id = departure.Id,
                Title = departure.SearchRequest.Lost.Name,
                City = departure.SearchRequest.Lost.City.Title,
                BDay = departure.SearchRequest.Lost.Birthday,
                Age = DateTime.Now.Year - departure.SearchRequest.Lost.Birthday.Year,
                Location = $"{departure.Location.Latitude},{departure.Location.Longitude}"
            });
        }

        return Results.Ok(departureList);
    }
 
}
