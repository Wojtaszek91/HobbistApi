using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.Post.AddFollower
{
    public class AddFollowerCommandRequest : IRequest<int>
    {
        public Guid PostId { get; set; }
        public Guid FollowerId { get; set; }
    }
}
