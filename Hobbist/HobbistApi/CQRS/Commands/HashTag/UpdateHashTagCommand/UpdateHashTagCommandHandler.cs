using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.HashTag.UpdateHashTagCommand
{
    public class UpdateHashTagCommandHandler : IRequestHandler<UpdateHashTagCommandRequest, int>
    {
        private readonly IHashTagRepository _hashTagRepository;
        private readonly ILogger<UpdateHashTagCommandHandler> _logger;

        public UpdateHashTagCommandHandler(IHashTagRepository hashTagRepository, ILogger<UpdateHashTagCommandHandler> logger)
        {
            this._hashTagRepository = hashTagRepository;
            this._logger = logger;
        }
        public Task<int> Handle(UpdateHashTagCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_hashTagRepository.EditHashTag(request.HashTagDto) ? 1 : 0);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error occured while trying to edit hashTag. HashTagName: {request.HashTagDto.HashTagName}, Error message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
