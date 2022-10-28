using DAL.Repositories.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.DeleteProfileCommand
{
    public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommandRequest, int>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger<UpsertPostCommandHandler> _logger;

        public DeleteProfileCommandHandler(IUserProfileRepository userProfileRepository, ILogger<UpsertPostCommandHandler> logger)
        {
            this._userProfileRepository = userProfileRepository;
            this._logger = logger;
        }
        public Task<int> Handle(DeleteProfileCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!_userProfileRepository.DoesProfileExist(request.ProfileId))
                {
                    _logger.LogError($"Attempt to delete non existing profile. ProfileId: {request.ProfileId}");
                    return Task.FromResult(2);
                }

                return _userProfileRepository.DeleteProfile(request.ProfileId) ? Task.FromResult(1) : Task.FromResult(0);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error occured while trying to delete profile. ProfileId: {request.ProfileId}, error message: {e.Message}");
                return Task.FromResult(2);
            }
        }
    }
}
