using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.PostMark.GetAveragePostMark
{
    public class GetAveragePostMarkQueryRequest : IRequest<int?>
    {
        public Guid PostId { get; set; }
    }
}
