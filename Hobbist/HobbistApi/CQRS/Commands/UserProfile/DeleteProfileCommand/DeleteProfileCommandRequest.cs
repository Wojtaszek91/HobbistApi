using MediatR;
using System;

namespace HobbistApi.CQRS.Commands.DeleteProfileCommand
{
    public class DeleteProfileCommandRequest : IRequest<int>
    {
        public Guid ProfileId { get; set; }
    }
}
