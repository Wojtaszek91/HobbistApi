using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.AddPostMark
{
    public class UpserPostMarkCommandHandler : IRequestHandler<UpsertPostMarkCommandRequest, int>
    {
        private readonly IPostMarkRepository _postMarkRepo;
        private readonly IPostRepository _postRepo;
        private readonly IUserProfileRepository _userProfileRepo;
        private readonly ILogger<UpserPostMarkCommandHandler> _logger;

        public UpserPostMarkCommandHandler(IPostMarkRepository postMarkRepo, IPostRepository postRepo, IUserProfileRepository userProfileRepo, ILogger<UpserPostMarkCommandHandler> logger)
        {
            this._postMarkRepo = postMarkRepo;
            this._postRepo = postRepo;
            this._userProfileRepo = userProfileRepo;
            this._logger = logger;
        }

        public Task<int> Handle(UpsertPostMarkCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var post = _postRepo.GetPostById(request.PostId);
                var profile = _userProfileRepo.GetProfileById(request.UserProfileId);

                if (post == null || profile == null) Task.FromResult(1);

                return Task.FromResult(_postMarkRepo.UpsertMark(profile, post, request.Mark) ? 1 : 0);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error occured while trying to upser post mark. PostId: {request.PostId}, ProfileId: {request.UserProfileId}, Exception message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
