using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.Post.AddFollower
{
    public class AddFollowerCommandHandler : IRequestHandler<AddFollowerCommandRequest, int>
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<AddFollowerCommandHandler> _logger;

        public AddFollowerCommandHandler(IPostRepository postRepository, ILogger<AddFollowerCommandHandler> logger)
        {
            this._postRepository = postRepository;
            this._logger = logger;
        }

        public Task<int> Handle(AddFollowerCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_postRepository.AddFollower(request.PostId, request.FollowerId) ? 1 : 0);        
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to add follower to post. PostId: {request.PostId}, FollowerId: {request.FollowerId}, Error message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
