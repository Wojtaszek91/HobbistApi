using DAL.Repositories.IRepositories;
using HobbistApi.CQRS.Commands.DeleteProfileCommand;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.UserProfile.AddHashtagCommand
{
    public class AddHashtagToProfileCommandHandler : IRequestHandler<AddHashtagToProfileCommandRequest, int>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IHashTagRepository _hashtagRepo;
        private readonly ILogger<AddHashtagToProfileCommandHandler> _logger;

        public AddHashtagToProfileCommandHandler(IUserProfileRepository userProfileRepository, IHashTagRepository hashtagRepo, ILogger<AddHashtagToProfileCommandHandler> logger)
        {
            this._userProfileRepository = userProfileRepository;
            this._hashtagRepo = hashtagRepo;
            this._logger = logger;
        }
        public Task<int> Handle(AddHashtagToProfileCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var hashTagList = _hashtagRepo.GetAllHashTagNamesList();
                if (!hashTagList.Contains(request.HashtagName))
                {
                    _logger.LogError($"Coudn't find hashtag name. HashtagName: {request.HashtagName}");
                    return Task.FromResult(2);
                }

                return _userProfileRepository.AddHashTagByNameToUserProfile(request.HashtagName, request.ProfileId)
                    ? Task.FromResult(1) : Task.FromResult(0);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error while trying add single hashtag. Message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
