using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.HashTag.DecreasePopularityCommand
{
    public class DecreasePopularityCommandHandler : IRequestHandler<DecreasePopularityCommandRequest, int>
    {
        private readonly IHashTagRepository _hashTagRepository;
        private readonly ILogger<DecreasePopularityCommandHandler> _logger;

        public DecreasePopularityCommandHandler(IHashTagRepository hashTagRepository, ILogger<DecreasePopularityCommandHandler> logger)
        {
            this._hashTagRepository = hashTagRepository;
            this._logger = logger;
        }
        public Task<int> Handle(DecreasePopularityCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_hashTagRepository.DecreasePopuplarity(request.HashTagId) ? 1 : 0);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to decrease hashtag popularity. HashTagId: {request.HashTagId}, Error message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
