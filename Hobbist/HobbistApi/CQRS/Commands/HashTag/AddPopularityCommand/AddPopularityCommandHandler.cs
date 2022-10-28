using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.HashTag.AddPopularityCommand
{
    public class AddPopularityCommandHandler : IRequestHandler<AddPopularityCommandRequest, int>
    {
        private readonly IHashTagRepository _hashTagRepository;
        private readonly ILogger<AddPopularityCommandHandler> _logger;

        public AddPopularityCommandHandler(IHashTagRepository hashTagRepository, ILogger<AddPopularityCommandHandler> logger)
        {
            this._hashTagRepository = hashTagRepository;
            this._logger = logger;
        }
        public Task<int> Handle(AddPopularityCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_hashTagRepository.AddPopularity(request.HashTagId) ? 1 : 0);
            }catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to add hashTag popularity. HashTagId: {request.HashTagId}, Exception message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
