using DAL.Repositories.IRepositories;
using HobbistApi.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;
using Models.Models.DTOs;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.Post.GetPostsByUserIdAndHashTag
{
    public class GetPostsByProfileIdAndHasTagHandler : IRequestHandler<GetPostsByProfileIdAndHasTagRequest, PostsListWithIndexDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostMarkRepository _postMarkRepository;
        private readonly ILogger<GetPostsByProfileIdAndHasTagHandler> _logger;

        public GetPostsByProfileIdAndHasTagHandler(IPostRepository postRepository, IPostMarkRepository postMarkRepository, ILogger<GetPostsByProfileIdAndHasTagHandler> logger)
        {
            this._postRepository = postRepository;
            this._postMarkRepository = postMarkRepository;
            this._logger = logger;
        }
        public Task<PostsListWithIndexDto> Handle(GetPostsByProfileIdAndHasTagRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<PostDto> postDtoList = new List<PostDto>();
                foreach (var post in _postRepository.GetPostsByProfileId(request.ProfileId, request.Index))
                {
                    postDtoList.Add(PostMapper.MapPostToPostDto(post, _postMarkRepository.GetAverageMark(post.Id), request.ProfileId));
                }

                return Task.FromResult(new PostsListWithIndexDto() { PostList = postDtoList, Index = request.Index });
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to get post list. UserId: {request.ProfileId}, HashTag: {request.HashTagName}, index: {request.Index}, requestingUserId: {request.RequestingProfileId}, Error message: {e.Message}");
                return null;
            }
        }
    }
}
