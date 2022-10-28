using MediatR;
using Models.Models.DTOs.Profile;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.UpdateProfileCommand.cs
{
    public class UpdateProfileHashtagsCommandRequest : IRequest<int>
    {
        public Guid ProfileId { get; set; }
        public List<string> HashtagsList { get; set; }
    }
}
