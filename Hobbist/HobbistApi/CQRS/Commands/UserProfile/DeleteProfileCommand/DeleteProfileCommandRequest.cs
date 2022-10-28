using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.DeleteProfileCommand
{
    public class DeleteProfileCommandRequest : IRequest<int>
    {
        public Guid ProfileId { get; set; }
    }
}
