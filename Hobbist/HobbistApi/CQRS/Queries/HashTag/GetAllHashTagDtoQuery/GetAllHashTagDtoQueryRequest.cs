using MediatR;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System.Collections.Generic;

namespace HobbistApi.CQRS.Queries.HashTag.GetAllHashTagDtoQuery
{
    public class GetAllHashTagDtoQueryRequest : IRequest<List<HashTagDto>>
    {
    }
}
