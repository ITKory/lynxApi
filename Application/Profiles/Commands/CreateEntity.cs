﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles.Commands
{
    public class CreateEntity<TEntity> : IRequest<TEntity> where TEntity : class
    {
        public  TEntity Entity { get; set; }
    }
}
