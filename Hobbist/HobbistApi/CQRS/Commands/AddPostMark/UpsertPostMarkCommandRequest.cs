using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.AddPostMark
{
    public class UpsertPostMarkCommandRequest : IRequest<int>
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public int Mark { get; set; }

    }
}
