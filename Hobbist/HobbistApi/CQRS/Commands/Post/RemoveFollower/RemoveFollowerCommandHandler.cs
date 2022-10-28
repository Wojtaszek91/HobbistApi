using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.Post.RemoveFollower
{
    public class RemoveFollowerCommandHandler : IRequestHandler<RemoveFollowerCommandRequest, int>
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<RemoveFollowerCommandHandler> _logger;

        public RemoveFollowerCommandHandler(IPostRepository postRepository, ILogger<RemoveFollowerCommandHandler> logger)
        {
            this._postRepository = postRepository;
            this._logger = logger;
        }
        public Task<int> Handle(RemoveFollowerCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_postRepository.RemoveFollower(request.PostId, request.FollowerId) ? 1 : 0);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to remove follower. FollowerId: {request.FollowerId}, postId: {request.PostId}, Error message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
