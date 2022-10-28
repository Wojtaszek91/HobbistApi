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
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommandRequest, int>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger<UpdateProfileCommandHandler> _logger;

        public UpdateProfileCommandHandler(IUserProfileRepository userProfileRepository, ILogger<UpdateProfileCommandHandler> logger)
        {
            this._userProfileRepository = userProfileRepository;
            this._logger = logger;
        }

        public Task<int> Handle(UpdateProfileCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = false;

            try 
            { 
                result = _userProfileRepository.UpdateProfile(request.UserProfileDto); 
            }
            catch(Exception e)
            { 
                _logger.LogError($"Error occured while trying to update user profile. UserAccountId: {request.UserProfileDto.ProfileId}, Exception message: {e.Message}");
                return Task.FromResult(2);
            }

            return result ? Task.FromResult(1) : Task.FromResult(0);
        }
    }
}
