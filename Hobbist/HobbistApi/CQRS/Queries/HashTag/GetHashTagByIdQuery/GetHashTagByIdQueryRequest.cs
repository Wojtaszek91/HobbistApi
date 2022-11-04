using MediatR;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;

namespace HobbistApi.CQRS.Commands.HashTag.GetHashTagById
{
    public class GetHashTagByIdQueryRequest : IRequest<HashTagDto>
    {
        public Guid HashTagId { get; set; }
    }
}
