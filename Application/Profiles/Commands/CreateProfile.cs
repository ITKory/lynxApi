using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Application.Profiles.Commands;

    public class CreateProfile : IRequest<Profile>
    {
        public Profile NewProfile { get; set; }  
         
    }
    

