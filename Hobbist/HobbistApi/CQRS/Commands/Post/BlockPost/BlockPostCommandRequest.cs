using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.Post.BlockPost
{
    public class BlockPostCommandRequest : IRequest<int>
    {
        public Guid PostId { get; set; }
    }
}
