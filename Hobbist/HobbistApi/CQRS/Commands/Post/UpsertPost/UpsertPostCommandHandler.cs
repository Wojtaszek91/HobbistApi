using DAL.Repositories.IRepositories;
using HobbistApi.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Commands.DeleteProfileCommand
{
    public class UpsertPostCommandHandler : IRequestHandler<UpsertPostCommandRequest, int>
    {
        private readonly IPostRepository _postRepository;
        private readonly IHashTagRepository _hashTagRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger<UpsertPostCommandHandler> _logger;

        public UpsertPostCommandHandler(IPostRepository postRepository, IHashTagRepository hashTagRepository,
            IUserProfileRepository userProfileRepository, ILogger<UpsertPostCommandHandler> logger)
        {
            this._postRepository = postRepository;
            this._hashTagRepository = hashTagRepository;
            this._userProfileRepository = userProfileRepository;
            this._logger = logger;
        }
        public Task<int> Handle(UpsertPostCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var hashTag = _hashTagRepository.GetHashTagByName(request.PostDto.ChainedTagName);
                var userProfile = _userProfileRepository.GetProfileById(request.PostDto.ProfileId);
                if (hashTag == null || userProfile == null)
                {
                    _logger.LogError($"No profile found with id: {request.PostDto.ProfileId} or hashtag with name: {request.PostDto.ChainedTagName}");
                    return Task.FromResult(2);
                }

                var postToDb = PostMapper.MapPostDtoToPost(request.PostDto, userProfile, hashTag);

                if (!_postRepository.DoesPostExists(postToDb.Id)) 
                    { return Task.FromResult(_postRepository.AddPost(postToDb) == true ? 1 : 0); }
                else
                    { return Task.FromResult(_postRepository.EditPost(postToDb) == true ? 1 : 0); }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error occured while trying to upsert post. PostDto: {@request.PostDto}, Error message: {e.Message}");
                return Task.FromResult(2);
            }

        }
    }
}
