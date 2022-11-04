using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.Post.AddViewPost
{
    public class AddViewToPostCommandHandler : IRequestHandler<AddViewToPostCommandRequest, int>
    {
        private readonly IPostRepository _postRepo;
        private readonly ILogger<AddViewToPostCommandHandler> _logger;

        public AddViewToPostCommandHandler(IPostRepository postRepo, ILogger<AddViewToPostCommandHandler> logger)
        {
            _postRepo = postRepo;
            _logger = logger;
        }

        public Task<int> Handle(AddViewToPostCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_postRepo.AddPostView(request.PostId) ? 1 : 0);

            }
            catch(Exception e)
            {
                _logger.LogError($"Error while trying to add view. Message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
