using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.UpdateProfilePhotoCommand
{
    public class UpdateProfilePhotoCommandHandler : IRequestHandler<UpdateProfilePhotoCommandRequest, int>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger<UpdateProfilePhotoCommandHandler> _logger;

        public UpdateProfilePhotoCommandHandler(IUserProfileRepository userProfileRepository, ILogger<UpdateProfilePhotoCommandHandler> logger)
        {
            this._userProfileRepository = userProfileRepository;
            this._logger = logger;
        }

        public Task<int> Handle(UpdateProfilePhotoCommandRequest request, CancellationToken cancellationToken)
        {
            var result = false;
            try 
            { 
                result = _userProfileRepository.UpdateProfilePhotoBase64(
                    request.AddProfilePhotoDto.PhotoBase64,
                    request.AddProfilePhotoDto.UserProfileId); 
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception occured while trying to update profile photo. UserProfileId: {request.AddProfilePhotoDto.UserProfileId}, Exception message: {e.Message}");
                return Task.FromResult(2);
            }

            return result ? Task.FromResult(1) : Task.FromResult(0);
        }
    }
}
