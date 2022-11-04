using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.HashTag.GetAllHashTagDtoQuery
{
    public class GetAllHashTagNameQueryHandler : IRequestHandler<GetAllHashTagNameQueryRequest, List<string>>
    {
        private readonly IHashTagRepository _hashTagRepository;
        private readonly ILogger<GetAllHashTagNameQueryHandler> _logger;

        public GetAllHashTagNameQueryHandler(IHashTagRepository hashTagRepository, ILogger<GetAllHashTagNameQueryHandler> logger)
        {
            this._hashTagRepository = hashTagRepository;
            this._logger = logger;
        }

        public Task<List<string>> Handle(GetAllHashTagNameQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_hashTagRepository.GetAllHashtagNames());
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to get hashtag list. Message: {e.Message}");
                return null;
            }              
        }
    }
}
