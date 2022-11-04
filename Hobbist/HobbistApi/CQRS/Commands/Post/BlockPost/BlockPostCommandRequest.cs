using MediatR;
using System;

namespace HobbistApi.CQRS.Commands.Post.BlockPost
{
    public class BlockPostCommandRequest : IRequest<int>
    {
        public Guid PostId { get; set; }
    }
}
