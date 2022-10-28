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

namespace HobbistApi.CQRS.Queries.Post.GetPostsByUserIdQuery
{
    public class GetPostsByProfileIdQueryHandler : IRequestHandler<GetPostsByProfileIdQueryRequest, List<PostDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostMarkRepository _postMarkRepository;
        private readonly ILogger<GetPostsByProfileIdQueryHandler> _logger;

        public GetPostsByProfileIdQueryHandler(IPostRepository postRepository, IPostMarkRepository postMarkRepository, ILogger<GetPostsByProfileIdQueryHandler> logger)
        {
            this._postRepository = postRepository;
            this._postMarkRepository = postMarkRepository;
            this._logger = logger;
        }
        public Task<List<PostDto>> Handle(GetPostsByProfileIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<PostDto> postDtoList = new List<PostDto>();
                foreach(var post in _postRepository.GetPostsByProfileId(request.ProfileId, request.Index))
                {
                    postDtoList.Add(PostMapper.MapPostToPostDto(post, _postMarkRepository.GetAverageMark(post.Id), request.ProfileId));
                }

                return Task.FromResult(postDtoList);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error occured while trying to get post list. UserId: {request.ProfileId}, index: {request.Index}, Error message: {e.Message}");
                return null;
            }
        }
    }
}
