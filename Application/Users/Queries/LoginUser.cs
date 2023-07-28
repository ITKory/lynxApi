using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class LoginUser : IRequest<IResult>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
