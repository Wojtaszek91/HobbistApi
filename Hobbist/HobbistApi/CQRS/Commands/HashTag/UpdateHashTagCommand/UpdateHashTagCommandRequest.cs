using MediatR;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.HashTag.UpdateHashTagCommand
{
    public class UpdateHashTagCommandRequest : IRequest<int>
    {
        public HashTagDto HashTagDto { get; set; }
    }
}
