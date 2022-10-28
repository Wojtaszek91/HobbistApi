using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.HashTag.GetHashTagById
{
    public class GetHashTagByIdQueryHandler : IRequestHandler<GetHashTagByIdQueryRequest, HashTagDto>
    {
        private readonly IHashTagRepository _hashTagRepository;
        private readonly ILogger<GetHashTagByIdQueryHandler> _logger;

        public GetHashTagByIdQueryHandler(IHashTagRepository hashTagRepository, ILogger<GetHashTagByIdQueryHandler> logger)
        {
            this._hashTagRepository = hashTagRepository;
            this._logger = logger;
        }

        public Task<HashTagDto> Handle(GetHashTagByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_hashTagRepository.GetHashTagById(request.HashTagId));
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to get hashtag by id. HashtagId: {request.HashTagId}, Error message: {e.Message}");
                return null;
            }
        }
    }
}
