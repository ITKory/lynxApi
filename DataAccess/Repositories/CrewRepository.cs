using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Application.Abstractions;

namespace DataAccess.Repositories;

public class CrewRepository: ICrewRepository
{
   protected NeondbContext _context;

    public CrewRepository( NeondbContext context)
    {
        _context = context;
    }

    public Crew ChangeCrewStatus( int crewId)
    {
        var crew = _context.Crews.Where(c => c.Id == crewId).First();
        crew.IsActive = !crew.IsActive;
        return crew;
    }
    public async Task<EntityEntry<CrewMate>> JoinCrew( int crewId , int userId)
    {
         var crew =  await _context.CrewMates.AddAsync(new CrewMate() {  Id = crewId, UserId = userId });
        _context.SaveChanges();
        return crew;
    }    
    public async Task<CrewMate> LeaveCrew( int crewMateId)
    {
       var cm =  await _context.CrewMates.Where(c => c.Id == crewMateId).FirstOrDefaultAsync();
        _context.CrewMates.Remove(cm);
        _context.SaveChanges();
        return cm;
    }

}
