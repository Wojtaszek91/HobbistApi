using MediatR;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.Post.GetBlockedPostsQuery
{
    public class GetBlockedPostsQueryRequest : IRequest<List<PostDto>>
    {
    }
}
