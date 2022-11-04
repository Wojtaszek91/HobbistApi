using MediatR;
using System;
using System.Collections.Generic;

namespace HobbistApi.CQRS.Commands.UpdateProfileCommand
{
    public class UpdateProfileHashtagsCommandRequest : IRequest<int>
    {
        public Guid ProfileId { get; set; }
        public List<string> HashtagsList { get; set; }
    }
}
