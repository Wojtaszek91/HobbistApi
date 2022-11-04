using MediatR;
using Models.Models.DTOs;

namespace HobbistApi.CQRS.Commands.UpdateProfilePhotoCommand
{
    public class UpdateProfilePhotoCommandRequest : IRequest<int>
    {
        public AddProfilePhotoDto AddProfilePhotoDto { get; set; }
    }
}
