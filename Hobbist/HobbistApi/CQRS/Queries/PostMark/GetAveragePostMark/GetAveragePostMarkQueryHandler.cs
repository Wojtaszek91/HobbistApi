using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.PostMark.GetAveragePostMark
{
    public class GetAveragePostMarkQueryHandler : IRequestHandler<GetAveragePostMarkQueryRequest, int?>
    {
        private readonly IPostMarkRepository postMarkRepo;
        private readonly ILogger<GetAveragePostMarkQueryHandler> _logger;

        public GetAveragePostMarkQueryHandler(IPostMarkRepository postMarkRepo, ILogger<GetAveragePostMarkQueryHandler> logger)
        {
            this.postMarkRepo = postMarkRepo;
            this._logger = logger;
        }

        public Task<int?> Handle(GetAveragePostMarkQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult((int?)postMarkRepo.GetAverageMark(request.PostId));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while trying to retrive post average mark. PostId: {request.PostId}. Message: {e.Message}");
                return null;
            }
        }
    }
}
