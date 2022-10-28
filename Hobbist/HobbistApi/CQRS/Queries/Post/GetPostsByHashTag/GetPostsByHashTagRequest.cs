using MediatR;
using Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.Post.GetPostsByHashTag
{
    public class GetPostsByHashTagRequest : IRequest<PostsListWithIndexDto>
    {
        public string HashTagName { get; set; }
        public int Index { get; set; }
        public Guid RequestingProfileId { get; set; }
    }
}
