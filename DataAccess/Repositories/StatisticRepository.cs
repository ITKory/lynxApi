using Application.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        protected NeondbContext _context;
        public StatisticRepository(NeondbContext context)
        {
            _context = context;
        }

        public async Task<IResult> GetUserStat(int userId)
        {
            if (_context.Users.Count(u => u.Id == userId) == 0)
                return Results.BadRequest();

            var userRegStat = _context.SeekerRegistrations
                .Where(sr => sr.UserId == userId);

            var userDeparturesIdList = userRegStat
                .Select(sr => sr.SearchDepartureId);

            var userDepartures = _context.SearchDepartures
                .Where(sd => userDeparturesIdList.Where(d => d == sd.Id).Any());

            var founds = _context.FoundStats
                .Where(s => s.UserId == userId);

            var response = new
            {
                RegStat = userRegStat,
                DeparturesCount = userDepartures.Count(),
                FoundsCount = founds.Count()
            };

            return Results.Ok(response);

        }

        public async Task<IResult> GetRequestStat()
        {
            DateOnly startingDate = DateOnly.FromDateTime(DateTime.Now);

            DateOnly endingDate = startingDate.AddMonths(1);

            var departures = _context.SearchDepartures.ToList();

            var departuresByPeriod = departures
                .Where(d => d.Date > startingDate && d.Date < endingDate);

            var activeDepartures = departuresByPeriod
                .Where(d => d.IsActive == true);

            var requests = _context.SearchRequests.ToList();

            var requestsByPeriod = requests
                .Where(d => d.Date > startingDate && d.Date < endingDate);

            var activeRequest = requestsByPeriod
                .Where(d => d.IsActive == true);

            var successRequest = requestsByPeriod
                .Where(r => r.IsFound == true);

            var successDeparture = departuresByPeriod
                .Where(d => d.IsActive == false && successRequest.Where(r => r.Id == d.SearchRequestId).Any());

            var response = new
            {
                Departures = departures.Count(),
                DeparturesByPeriod = requestsByPeriod.Count(),
                ActiveDepartures = activeDepartures.Count(),
                Requests = requests.Count(),
                RequestsByPeriod = requestsByPeriod.Count(),
                ActiveRequests = activeRequest.Count(),
                SuccessDeparture = successDeparture.Count(),
                SuccessRequest = successRequest.Count()

            };
            return Results.Ok(response);

        }

    }
}
