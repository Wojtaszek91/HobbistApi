using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.Post.UnblockPost
{
    public class UnblockPostCommandHandler : IRequestHandler<UnblockPostCommandRequest, int>
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<UnblockPostCommandHandler> _logger;

        public UnblockPostCommandHandler(IPostRepository postRepository, ILogger<UnblockPostCommandHandler> logger)
        {
            this._postRepository = postRepository;
            this._logger = logger;
        }
        public Task<int> Handle(UnblockPostCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_postRepository.UnblockPost(request.PostId) ? 1 : 0);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to unblock post. PostiId: {request.PostId}, Error message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
