using MediatR;
using System;

namespace HobbistApi.CQRS.Commands.UserProfile.RemoveHashtagCommand
{
    public class RemoveHashtagFromProfileCommandRequest : IRequest<int>
    {
        public Guid ProfileId { get; set; }
        public string HashtagName { get; set; }
    }
}
