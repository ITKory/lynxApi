using Application.Abstractions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories;

public class DepartureRepository: IDepartureRepository
{
    protected NeondbContext _context;

    public DepartureRepository( NeondbContext context)
    {
        _context = context;
    }
    public  ICollection<User> GetDepartureParticipants(int departureId)
    {
        List<User> participants = new();
             var users =   _context.SeekerRegistrations
            .Where(s => s.SearchDeparture.Id == departureId)
            .Select(s => s.UserId);

        foreach (var user in users)
             participants.Add(_context.Users.Find(user));

        return participants;

    }
    public void GetDeparture(int departureId)
    {
        _context.SearchDepartures.Where(d => d.Id == departureId).Select(d => d);
    }
   
}
