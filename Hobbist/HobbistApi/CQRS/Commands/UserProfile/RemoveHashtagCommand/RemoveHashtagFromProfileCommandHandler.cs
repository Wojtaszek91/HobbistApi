using DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.UserProfile.RemoveHashtagCommand
{
    public class RemoveHashtagFromProfileCommandHandler
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IHashTagRepository _hashtagRepo;
        private readonly ILogger<RemoveHashtagFromProfileCommandHandler> _logger;

        public RemoveHashtagFromProfileCommandHandler(IUserProfileRepository userProfileRepository, IHashTagRepository hashtagRepo, ILogger<RemoveHashtagFromProfileCommandHandler> logger)
        {
            this._userProfileRepository = userProfileRepository;
            this._hashtagRepo = hashtagRepo;
            this._logger = logger;
        }
        public Task<int> Handle(RemoveHashtagFromProfileCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var hashTagList = _hashtagRepo.GetAllHashTagNamesList();
                if (!hashTagList.Contains(request.HashtagName))
                {
                    _logger.LogError($"Coudn't find hashtag name. HashtagName: {request.HashtagName}");
                    return Task.FromResult(2);
                }

                return _userProfileRepository.RemoveHashTagByNameFromUserProfile(request.HashtagName, request.ProfileId)
                    ? Task.FromResult(1) : Task.FromResult(0);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error while trying to remove hashtag from profileId : {request.ProfileId}. Message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
