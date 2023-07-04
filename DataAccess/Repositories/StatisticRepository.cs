using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class StatisticRepository:IStatisticRepository
    {
        protected NeondbContext _context;
        public StatisticRepository(NeondbContext context )
        {
            _context = context;
        }

        public void GetUserStat( int userId)
        {
                var userStat= 
                _context.SeekerRegistrations
                .Where(s => s.User.Id == userId)
                .Select(p => new
                 {
                StartTime = p.StartAt,
                EndTime = p.EndAt,
                TotalTime = new TimeOnly(
                p.EndAt.Hour - p.StartAt.Hour,
                p.StartAt.Minute - p.EndAt.Minute,
                p.StartAt.Minute - p.EndAt.Minute
                ),
                Departure = p.SearchDepartureId,
                Found = p.SearchDeparture.SearchRequest.IsFound,
                 });
            var userDepartureStat = new
            {
                userStat,
                DepartureCount = userStat.Count()
            };
            
        }
        public void GetDepartureStat()
        {

        }
        public void GetComplexStat()
        {

        }
        public void GetComplexStatByPeriod(DateTime startPeriod , DateTime endPeriod)
        {

        }
    }
}
