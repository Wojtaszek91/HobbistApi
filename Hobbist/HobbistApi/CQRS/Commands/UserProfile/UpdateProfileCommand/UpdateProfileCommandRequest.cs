using MediatR;
using Models.Models.DTOs.Profile;

namespace HobbistApi.CQRS.Commands.UpdateProfileCommand.cs
{
    public class UpdateProfileCommandRequest : IRequest<int>
    {
        public UpsertProfileDto UserProfileDto { get; set; }
    }
}
