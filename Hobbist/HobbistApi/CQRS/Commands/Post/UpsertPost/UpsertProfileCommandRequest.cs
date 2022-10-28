using MediatR;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.DeleteProfileCommand
{
    public class UpsertPostCommandRequest : IRequest<int>
    {
        public PostDto PostDto { get; set; }
    }
}
