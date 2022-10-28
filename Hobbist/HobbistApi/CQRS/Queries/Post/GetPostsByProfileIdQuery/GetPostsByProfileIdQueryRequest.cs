using MediatR;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.Post.GetPostsByUserIdQuery
{
    public class GetPostsByProfileIdQueryRequest : IRequest<List<PostDto>>
    {
        public Guid ProfileId { get; set; }
        public int Index { get; set; }
    }
}
