using MediatR;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;

namespace HobbistApi.CQRS.Queries.Post.GetPostByIdQuery
{
    public class GetPostByIdQueryRequest : IRequest<PostDto>
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
    }
}
