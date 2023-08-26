using Application.Abstractions;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace DataAccess.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        protected NeondbContext _context;

        public RequestRepository(NeondbContext context)
        {
            _context = context;
        }
        public async Task<IResult> CreateRequestAsync(SearchRequest searchRequest)
        {
            var city = await _context.Cities.Where(c => c.Title == searchRequest.Lost.City.Title).FirstOrDefaultAsync();
            if (city == null)
                return Results.BadRequest();
            searchRequest.Lost.City = city;
            searchRequest.Lost.CityId = city.Id;
            _context.Add(searchRequest);
            await _context.SaveChangesAsync();
            return Results.Ok();
        }
        public async Task<IResult> RemoveRequestAsync(int requestId)
        {
            var searchRequest = _context.SearchRequests
                .Include(sr => sr.SearchDepartures)
                .Include(sr=>sr.MissingInformer)
                .Include(sr=>sr.Lost)
                .Include(sr=>sr.Location)
                .FirstOrDefault(s => s.Id == requestId);
            if (searchRequest == null)
                return Results.BadRequest($"Search request is null with id={requestId}");

            if (searchRequest.SearchDepartures != null)
            {
                 
                    _context.SearchDepartures.RemoveRange(searchRequest.SearchDepartures);
                
            };
            try
            {
                _context.SearchRequests.Remove(searchRequest);
                _context.Profiles.Remove(searchRequest.Lost);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
            await _context.SaveChangesAsync();
            return Results.Ok(searchRequest);
        }

        public async Task<IResult> GetRequestAsync(int requestId)
        {
            var request = _context.SearchRequests
                 .Include(s => s.Lost)
                 .ThenInclude(l => l.City)
                 .Include(s => s.RequestAdministrator)
                 .ThenInclude(ra => ra.Profile)
                 .Include(s => s.MissingInformer)
                 .Include(s => s.Location)
                 .Include(s=>s.SearchDepartures)
                 .First(r => r.Id == requestId);

            if (request == null)
            {
                return Results.BadRequest();
            }

            return Results.Ok(request);
        }

        public async Task<IResult> UpdateStatusAsync(ListItemModel item)
        {

            var request = _context.SearchRequests
                .FirstOrDefault(s => s.Id == item.Id);
            if (request != null)
            {
                request.IsActive = item.IsActive;

                if (request.IsActive == false)
                {
                    await _context.SearchDepartures
                          .Where(s => s.SearchRequestId == item.Id)
                          .ForEachAsync(s => s.IsActive = false);
                }

                request.IsDied = item.IsDied;
                request.IsFound = item.IsFound;
                _context.SearchRequests.Update(request);
                _context.SaveChanges();
            }
            else
            {
                return Results.BadRequest();
            }
            return Results.Ok();

        }
        public async Task<IResult> GetAllRequestsAsync()
        {
            var requests = await _context.SearchRequests
           .Include(s => s.Lost)
           .ThenInclude(s => s.City)
           .ToListAsync();

            if (requests == null)
                return Results.BadRequest();

            var requestList =
                new List<ListItemModel>();

            foreach (var item in requests)
            {
                requestList.Add(new()
                {
                    Id = item.Id,
                    Title = item.Lost.Name,
                    City = item.Lost.City.Title,
                    IsActive = item.IsActive,
                    IsDied = item.IsDied,
                    IsFound = item.IsFound,
                    BDay = item.Lost.Birthday,
                    Age = DateTime.Now.Year - item.Lost.Birthday.Year
                });
            }

            return Results.Ok(requestList);

        }
    }
}
