using MediatR;
using Models.Models.EntityFrameworkJoinEntities.DTOs;

namespace HobbistApi.CQRS.Commands.DeleteProfileCommand
{
    public class UpsertPostCommandRequest : IRequest<int>
    {
        public PostDto PostDto { get; set; }
    }
}
