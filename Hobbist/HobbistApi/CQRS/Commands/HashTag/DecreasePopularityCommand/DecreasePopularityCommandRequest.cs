using MediatR;
using System;

namespace HobbistApi.CQRS.Commands.HashTag.DecreasePopularityCommand
{
    public class DecreasePopularityCommandRequest : IRequest<int>
    {
        public Guid HashTagId { get; set; }
    }
}
