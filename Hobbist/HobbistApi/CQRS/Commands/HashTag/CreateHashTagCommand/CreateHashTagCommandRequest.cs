using MediatR;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.HashTag.CreateHashTagCommand
{
    public class CreateHashTagCommandRequest : IRequest<int>
    {
        public string NewHashTagName { get; set; }
    }
}
