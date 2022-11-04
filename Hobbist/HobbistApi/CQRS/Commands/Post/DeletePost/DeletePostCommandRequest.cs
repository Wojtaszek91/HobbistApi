using MediatR;
using System;

namespace HobbistApi.CQRS.Commands.Post.DeletePost
{
    public class DeletePostCommandRequest : IRequest<int>
    {
        public Guid PostId { get; set; }
    }
}
