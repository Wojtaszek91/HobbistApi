using MediatR;
using System;

namespace HobbistApi.CQRS.Commands.Post.UnblockPost
{
    public class UnblockPostCommandRequest : IRequest<int>
    {
        public Guid PostId { get; set; }
    }
}
