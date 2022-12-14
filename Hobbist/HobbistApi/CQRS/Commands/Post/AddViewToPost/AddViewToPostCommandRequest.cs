using MediatR;
using System;

namespace HobbistApi.CQRS.Commands.Post.AddViewPost
{
    public class AddViewToPostCommandRequest : IRequest<int>
    {
        public Guid PostId { get; set; }
    }
}
