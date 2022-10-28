using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.UserProfile.RemoveHashtagCommand
{
    public class RemoveHashtagFromProfileCommandRequest : IRequest<int>
    {
        public Guid ProfileId { get; set; }
        public string HashtagName { get; set; }
    }
}
