using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.HashTag.DeleteHashTagCommand
{
    public class DeleteHashTagCommandHandler : IRequestHandler<DeleteHashTagCommandRequest, int>
    {
        private readonly IHashTagRepository _hashTagRepository;
        private readonly ILogger<DeleteHashTagCommandHandler> _logger;

        public DeleteHashTagCommandHandler(IHashTagRepository hashTagRepository, ILogger<DeleteHashTagCommandHandler> logger)
        {
            this._hashTagRepository = hashTagRepository;
            this._logger = logger;
        }
        public Task<int> Handle(DeleteHashTagCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_hashTagRepository.DeleteHashTag(request.HashTagId) ? 1 : 0);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error occured while trying to delete HashTag. HashTagId: {request.HashTagId}, Exception message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
