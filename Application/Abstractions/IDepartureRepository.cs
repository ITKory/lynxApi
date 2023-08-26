using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IDepartureRepository
    {
        Task<IResult> GetDeparture(int departureId);
        IResult GetDepartureParticipants(int departureId);
        Task<IResult> GetAllDeparture();
        IResult RegistrationOnDeparture(int userId, int departureId, TimeOnly startAt);
        Task<IResult> CloseDeparture(int departureId);
        Task<IResult> CreateDeparture(SearchDeparture searchDeparture);
        Task<IResult> GetDepartureLocationAsync();
        Task<IResult> RemoveDeparture(int departureId);
        Task<IResult> UpdateStatusAsync(ListItemModel item);
    }
}
