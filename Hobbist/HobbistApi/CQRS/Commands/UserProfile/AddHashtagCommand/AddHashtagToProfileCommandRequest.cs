using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.UserProfile.AddHashtagCommand
{
    public class AddHashtagToProfileCommandRequest : IRequest<int>
    {
        public Guid ProfileId { get; set; }
        public string HashtagName { get; set; }
    }
}
