using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.Post.BlockPost
{
    public class BlockPostCommandHandler : IRequestHandler<BlockPostCommandRequest, int>
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<BlockPostCommandHandler> _logger;

        public BlockPostCommandHandler(IPostRepository postRepository, ILogger<BlockPostCommandHandler> logger)
        {
            this._postRepository = postRepository;
            this._logger = logger;
        }
        public Task<int> Handle(BlockPostCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_postRepository.BlockPost(request.PostId) ? 1 : 0);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to block post. PostId: {request.PostId}, Error message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
