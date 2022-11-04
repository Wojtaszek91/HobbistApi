using MediatR;
using System;

namespace HobbistApi.CQRS.Commands.Post.AddFollower
{
    public class AddFollowerCommandRequest : IRequest<int>
    {
        public Guid PostId { get; set; }
        public Guid FollowerId { get; set; }
    }
}
