using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IRequestRepository
    {
        Task<IResult> GetRequestAsync(int requestId);
        Task<IResult> GetAllRequestsAsync();
        Task<IResult> UpdateStatusAsync(ListItemModel item);
        Task<IResult> CreateRequestAsync(SearchRequest searchRequest);
        Task<IResult> RemoveRequestAsync(int requestId);
    }
}
