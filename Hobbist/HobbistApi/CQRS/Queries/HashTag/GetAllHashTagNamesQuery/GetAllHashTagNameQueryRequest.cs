using MediatR;
using System.Collections.Generic;

namespace HobbistApi.CQRS.Queries.HashTag.GetAllHashTagDtoQuery
{
    public class GetAllHashTagNameQueryRequest : IRequest<List<string>>
    {
    }
}
