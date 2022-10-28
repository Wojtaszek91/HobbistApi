using MediatR;
using Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.GetProfileCommand
{
    public class GetProfileByIdQueryRequest : IRequest<UserProfileViewModel>
    {
        public Guid ProfileId { get; set; }
    }
}
