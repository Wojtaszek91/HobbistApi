using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.HashTag.GetAllHashTagDtoQuery
{
    public class GetAllHashTagDtoQueryHandler : IRequestHandler<GetAllHashTagDtoQueryRequest, List<HashTagDto>>
    {
        private readonly IHashTagRepository _hashTagRepository;
        private readonly ILogger<GetAllHashTagDtoQueryHandler> _logger;

        public GetAllHashTagDtoQueryHandler(IHashTagRepository hashTagRepository, ILogger<GetAllHashTagDtoQueryHandler> logger)
        {
            this._hashTagRepository = hashTagRepository;
            this._logger = logger;
        }

        public Task<List<HashTagDto>> Handle(GetAllHashTagDtoQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_hashTagRepository.GetAllHashtagsDto());
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to get hashtag list. Message: {e.Message}");
                return null;
            }              
        }
    }
}
