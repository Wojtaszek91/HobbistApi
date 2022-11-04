using MediatR;
using System;

namespace HobbistApi.CQRS.Commands.UserProfile.AddHashtagCommand
{
    public class AddHashtagToProfileCommandRequest : IRequest<int>
    {
        public Guid ProfileId { get; set; }
        public string HashtagName { get; set; }
    }
}
