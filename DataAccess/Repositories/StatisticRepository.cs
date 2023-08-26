using Application.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

            var userRegStat = _context.SeekerRegistrations
                .Include(s=>s.SearchDeparture)
                .Where(sr => sr.UserId == userId);
            if(userRegStat == null)
            {
                return Results.BadRequest();
            }

            var founds = _context.FoundStats
                .Where(s => s.UserId == userId);

            List<(TimeSpan time,int departureId)> times = new();

            foreach( var u in userRegStat) {
                 times.Add((
                    DateTime.Parse(u.EndAt.ToString()).Subtract(DateTime.Parse(u.StartAt.ToString())),
                      u.SearchDepartureId ));
            }


            var response = new
            {
                RegStat = userRegStat,
                DeparturesCount = userRegStat.Count(),
                FoundsCount = founds.Count(),
                Departures = times.Select(i =>  new { i.time, i.departureId }),
                Total = TimeSpan.FromMinutes( times.Sum(t=>t.time.TotalMinutes))
            };

            return Results.Ok(response);

        }

        public async Task<IResult> GetRequestStat()
        {
            DateOnly startingDate = DateOnly.FromDateTime(new DateTime(DateTime.Now.Year-1, DateTime.Now.Month, DateTime.Now.Day));

            DateOnly endingDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));

            var departures = _context.SearchDepartures
                .Include(D=>D.SearchRequest)
                .ToList();

            var departuresByPeriod = departures
                .Where(d => d.Date > startingDate && d.Date < endingDate );

            var activeDepartures = departuresByPeriod
                .Where(d => d.IsActive == true);

            var requests = _context.SearchRequests.ToList();

            var requestsByPeriod = requests
                .Where(d => d.Date > startingDate && d.Date < endingDate);

            var activeRequest = requestsByPeriod
                .Where(d => d.IsActive == true);

            var closeRequest = requestsByPeriod
                .Where(r => r.IsFound == true);

            var closeDeparture = departuresByPeriod
                .Where(d => d.IsActive == false && closeRequest.Where(r => r.Id == d.SearchRequestId).Any());

            var closeIsAliveRequest = closeRequest.Where(r => r.IsDied == false);
            var closeIsDiedRequest = closeRequest.Where(r => r.IsDied == true);

            var response = new
            {
                Departures = departures.Count(),
                DeparturesByPeriod = requestsByPeriod.Count(),
                ActiveDepartures = activeDepartures.Count(),
                CloseDeparture = closeDeparture.Count(),
                Requests = requests.Count(),
                RequestsByPeriod = requestsByPeriod.Count(),
                ActiveRequests = activeRequest.Count(),
                CloseRequest = closeRequest.Count(),
                CloseIsAliveRequest = closeIsAliveRequest.Count(),
                closeIsDiedRequest = closeIsDiedRequest.Count()
            };
            return Results.Ok(response);

        }

    }
}
