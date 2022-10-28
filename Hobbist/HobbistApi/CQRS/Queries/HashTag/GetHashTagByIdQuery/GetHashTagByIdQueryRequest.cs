using MediatR;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.HashTag.GetHashTagById
{
    public class GetHashTagByIdQueryRequest : IRequest<HashTagDto>
    {
        public Guid HashTagId { get; set; }
    }
}
