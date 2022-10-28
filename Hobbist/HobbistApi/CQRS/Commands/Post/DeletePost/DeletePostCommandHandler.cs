using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.Post.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommandRequest, int>
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<DeletePostCommandHandler> _logger;

        public DeletePostCommandHandler(IPostRepository postRepository, ILogger<DeletePostCommandHandler> logger)
        {
            this._postRepository = postRepository;
            this._logger = logger;
        }
        public Task<int> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_postRepository.DeletePost(request.PostId) ? 1 : 0);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to delete post. PostId: {request.PostId}, Error message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
