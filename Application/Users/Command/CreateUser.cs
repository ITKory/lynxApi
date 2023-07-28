using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Command
{
    public class CreateUser : IRequest<IResult>
    {
        public User NewUser { get; set; }
    }
}
