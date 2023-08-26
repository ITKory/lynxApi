using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface ICrewRepository
    {
        Crew ChangeCrewStatus(int crewId);
        Task<EntityEntry<CrewMate>> JoinCrew(int crewId, int userId);
        Task<CrewMate> LeaveCrew(int crewMateId);
    }
}
