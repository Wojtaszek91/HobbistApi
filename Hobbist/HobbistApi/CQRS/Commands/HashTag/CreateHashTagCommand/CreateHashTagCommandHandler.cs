using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.HashTag.CreateHashTagCommand
{
    public class CreateHashTagCommandHandler : IRequestHandler<CreateHashTagCommandRequest, int>
    {
        public IHashTagRepository _hashTagRepository { get; }
        public ILogger<CreateHashTagCommandHandler> _logger { get; }

        public CreateHashTagCommandHandler(IHashTagRepository hashTagRepository, ILogger<CreateHashTagCommandHandler> logger)
        {
            _hashTagRepository = hashTagRepository;
            _logger = logger;
        }

        public Task<int> Handle(CreateHashTagCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_hashTagRepository.AddHashTag(request.NewHashTagName) ? 1 : 0);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to add hashtag. Hashtag name: {request.NewHashTagName}, Exception message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
