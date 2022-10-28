using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.HashTag.DeleteHashTagCommand
{
    public class DeleteHashTagCommandRequest : IRequest<int>
    {
        public Guid HashTagId { get; set; }
    }
}
