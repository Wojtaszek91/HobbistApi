using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.UpdateProfileCommand.cs
{
    public class UpdateProfileHashtagsCommandHandler : IRequestHandler<UpdateProfileHashtagsCommandRequest, int>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger<UpdateProfileHashtagsCommandHandler> _logger;

        public UpdateProfileHashtagsCommandHandler(IUserProfileRepository userProfileRepository, ILogger<UpdateProfileHashtagsCommandHandler> logger)
        {
            this._userProfileRepository = userProfileRepository;
            this._logger = logger;
        }

        public Task<int> Handle(UpdateProfileHashtagsCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!_userProfileRepository.DoesProfileExist(request.ProfileId))
                {
                    _logger.LogError($"Coudn't find profile with id: {request.ProfileId}. Cant update hashtags.");
                    return Task.FromResult(2);
                }

                return _userProfileRepository.UpdateProfileHashtagsByList(request.ProfileId, request.HashtagsList)
                    ? Task.FromResult(1) : Task.FromResult(0);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error while trying to update hashtags on profileId: {request.ProfileId}. Message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
