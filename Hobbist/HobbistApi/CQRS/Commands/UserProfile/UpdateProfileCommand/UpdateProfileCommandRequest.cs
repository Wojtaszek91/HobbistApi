using MediatR;
using Models.Models.DTOs.Profile;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.UpdateProfileCommand.cs
{
    public class UpdateProfileCommandRequest : IRequest<int>
    {
        public UpsertProfileDto UserProfileDto { get; set; }
    }
}
