﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.HashTag.DecreasePopularityCommand
{
    public class DecreasePopularityCommandRequest : IRequest<int>
    {
        public Guid HashTagId { get; set; }
    }
}
