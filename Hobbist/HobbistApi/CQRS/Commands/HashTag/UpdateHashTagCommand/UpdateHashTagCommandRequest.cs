using MediatR;
using Models.Models.EntityFrameworkJoinEntities.DTOs;

namespace HobbistApi.CQRS.Commands.HashTag.UpdateHashTagCommand
{
    public class UpdateHashTagCommandRequest : IRequest<int>
    {
        public HashTagDto HashTagDto { get; set; }
    }
}
