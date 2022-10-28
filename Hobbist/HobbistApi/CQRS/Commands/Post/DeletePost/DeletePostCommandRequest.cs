using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.Post.DeletePost
{
    public class DeletePostCommandRequest : IRequest<int>
    {
        public Guid PostId { get; set; }
    }
}
