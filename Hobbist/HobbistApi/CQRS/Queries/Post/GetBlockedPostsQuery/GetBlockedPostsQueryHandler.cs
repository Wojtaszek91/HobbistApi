using DAL.Repositories.IRepositories;
using HobbistApi.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.CQRS.Queries.Post.GetBlockedPostsQuery
{
    public class GetBlockedPostsQueryHandler : IRequestHandler<GetBlockedPostsQueryRequest, List<PostDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostMarkRepository _postMarkRepository;
        private readonly ILogger<GetBlockedPostsQueryHandler> _logger;

        public GetBlockedPostsQueryHandler(IPostRepository postRepository, IPostMarkRepository postMarkRepository, ILogger<GetBlockedPostsQueryHandler> logger)
        {
            _postRepository = postRepository;
            _postMarkRepository = postMarkRepository;
            _logger = logger;
        }

        public Task<List<PostDto>> Handle(GetBlockedPostsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<PostDto> postDtoList = new List<PostDto>();
                foreach (var post in _postRepository.GetBlockedPostList())
                {
                    postDtoList.Add(PostMapper.MapPostToPostDto(post, _postMarkRepository.GetAverageMark(post.Id), Guid.Empty));
                }
                return Task.FromResult(postDtoList);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error while trying to get block post list. Message: {e.Message}");
                return null;
            }
        }
    }
}
