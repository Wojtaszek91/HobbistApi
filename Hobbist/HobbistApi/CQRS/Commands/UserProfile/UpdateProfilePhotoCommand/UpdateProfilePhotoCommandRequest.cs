using MediatR;
using Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.UpdateProfilePhotoCommand
{
    public class UpdateProfilePhotoCommandRequest : IRequest<int>
    {
        public AddProfilePhotoDto AddProfilePhotoDto { get; set; }
    }
}
