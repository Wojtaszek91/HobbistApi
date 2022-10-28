using DAL.Repositories.IRepositories;
using HobbistApi.CQRS.Queries.GetProfileCommand;
using HobbistApi.CQRS.Queries.GetProfileQuery;
using MediatR;
using Microsoft.Extensions.Logging;
using Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.GetProfileQuery
{
    public class GetProfileByIdQueryHandler : IRequestHandler<GetProfileByIdQueryRequest, UserProfileViewModel>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IHashTagRepository _hashTagRepository;
        private readonly ILogger<GetProfileByIdQueryHandler> _logger;

        public GetProfileByIdQueryHandler(IUserProfileRepository userProfileRepository, IHashTagRepository hashTagRepository, ILogger<GetProfileByIdQueryHandler> logger)
        {
            this._userProfileRepository = userProfileRepository;
            this._hashTagRepository = hashTagRepository;
            this._logger = logger;
        }

        public async Task<UserProfileViewModel> Handle(GetProfileByIdQueryRequest request, CancellationToken cancellationToken)
        {
            UserProfileViewModel userProfileViewModel = new UserProfileViewModel();

            try
            {
                userProfileViewModel.UserProfle = _userProfileRepository.GetProfileByIdDto(request.ProfileId);
                userProfileViewModel.HashTagNames = _hashTagRepository.GetAllHashTagNamesList();
            }
            catch (Exception e) 
            {
                _logger.LogError($"Exception while trying to init UserProfileViewModel. Profile id: {request.ProfileId}. Exception message: {e.Message}");
                return null;
            }

            return userProfileViewModel;
        }
    }
}
