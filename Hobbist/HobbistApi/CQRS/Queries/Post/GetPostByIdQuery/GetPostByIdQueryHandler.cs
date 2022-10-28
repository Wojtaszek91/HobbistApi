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

namespace HobbistApi.CQRS.Queries.Post.GetPostByIdQuery
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQueryRequest, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostMarkRepository _postMarkRepository;
        private readonly ILogger<GetPostByIdQueryHandler> _logger;

        public GetPostByIdQueryHandler(IPostRepository postRepository, IPostMarkRepository postMarkRepository, ILogger<GetPostByIdQueryHandler> logger)
        {
            _postRepository = postRepository;
            _postMarkRepository = postMarkRepository;
            _logger = logger;
        }
        public Task<PostDto> Handle(GetPostByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var post = _postRepository.GetPostById(request.PostId);

                return Task.FromResult(PostMapper.MapPostToPostDto(post, _postMarkRepository.GetAverageMark(request.PostId), request.UserProfileId));
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to get post by id. PostId: {request.PostId}, Exception message: {e.Message}");
                return null;
            }     
        }
    }
}
