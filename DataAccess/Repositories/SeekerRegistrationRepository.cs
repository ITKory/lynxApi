using Application.Abstractions;
using Application.Entity.Queries;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SeekerRegistrationRepository : ISeekerRegistrationRepository
    {
        NeondbContext _context;

        public SeekerRegistrationRepository(NeondbContext context)
        {
            _context = context;
        }

        public void SetTime(int userId, TimeOnly startTime)
        {
            SeekerRegistration seekerRegistrations = GetEntityInfo(userId);
            seekerRegistrations.StartAt = startTime;
            _context.SaveChanges();

        }
        public void SetTime(int userId, TimeOnly startTime, TimeOnly endTime)
        {
            SeekerRegistration seekerRegistrations = GetEntityInfo(userId);
            seekerRegistrations.StartAt = startTime;
            seekerRegistrations.EndAt = endTime;
            _context.SaveChanges();

        }
        public void SeTime(int userId, TimeOnly endTime)
        {
            SeekerRegistration seekerRegistrations = GetEntityInfo(userId);
            seekerRegistrations.EndAt = endTime;
            _context.SaveChanges();
        }

        private SeekerRegistration GetEntityInfo(int userId)
        {
            return _context.SeekerRegistrations
                .First(s => s.User.Id == userId);
        }


    }
}
