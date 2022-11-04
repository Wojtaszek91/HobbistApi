using MediatR;
using System;

namespace HobbistApi.CQRS.Commands.HashTag.DeleteHashTagCommand
{
    public class DeleteHashTagCommandRequest : IRequest<int>
    {
        public Guid HashTagId { get; set; }
    }
}
