using MediatR;
using Models.Models.DTOs;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.Post.GetPostsByUserIdAndHashTag
{
    public class GetPostsByProfileIdAndHasTagRequest : IRequest<PostsListWithIndexDto>
    {
        public Guid ProfileId { get; set; }
        public int Index { get; set; }
        public string HashTagName { get; set; }
        public Guid RequestingProfileId { get; set; }
    }
}
