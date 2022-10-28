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

namespace HobbistApi.CQRS.Queries.Post.GetPostsByHashTag
{
    public class GetPostsByHashTagHandler : IRequestHandler<GetPostsByHashTagRequest, PostsListWithIndexDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostMarkRepository _postMarkRepository;
        private readonly ILogger<GetPostsByHashTagHandler> _logger;

        public GetPostsByHashTagHandler(IPostRepository postRepository, IPostMarkRepository postMarkRepository, ILogger<GetPostsByHashTagHandler> logger)
        {
            _postRepository = postRepository;
            _postMarkRepository = postMarkRepository;
            _logger = logger;
        }
        public Task<PostsListWithIndexDto> Handle(GetPostsByHashTagRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<PostDto> postDtoList = new List<PostDto>();
                foreach(var post in _postRepository.GetPostsByHashTag(request.HashTagName, request.Index))
                {
                    postDtoList.Add(PostMapper.MapPostToPostDto(post, _postMarkRepository.GetAverageMark(post.Id), request.RequestingProfileId));
                }

                return Task.FromResult(new PostsListWithIndexDto() { PostList = postDtoList, Index = request.Index });
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to get post list. HashtagName: {request.HashTagName}, index: {request.Index}, requestingUserId: {request.RequestingProfileId}, Error message: {e.Message}");
                return null;
            }
        }
    }
}
