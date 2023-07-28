using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IStatisticRepository
    {
        Task<IResult> GetUserStat(int userId);
        Task<IResult> GetRequestStat();
    }
}
