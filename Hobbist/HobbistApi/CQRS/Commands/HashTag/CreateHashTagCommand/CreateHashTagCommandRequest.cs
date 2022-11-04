using MediatR;

namespace HobbistApi.CQRS.Commands.HashTag.CreateHashTagCommand
{
    public class CreateHashTagCommandRequest : IRequest<int>
    {
        public string NewHashTagName { get; set; }
    }
}
