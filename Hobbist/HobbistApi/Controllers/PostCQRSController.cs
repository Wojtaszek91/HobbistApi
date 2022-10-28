using HobbistApi.CQRS.Commands.DeleteProfileCommand;
using HobbistApi.CQRS.Commands.Post.AddFollower;
using HobbistApi.CQRS.Commands.Post.BlockPost;
using HobbistApi.CQRS.Commands.Post.DeletePost;
using HobbistApi.CQRS.Commands.Post.RemoveFollower;
using HobbistApi.CQRS.Commands.Post.UnblockPost;
using HobbistApi.CQRS.Queries.Post.GetBlockedPostsQuery;
using HobbistApi.CQRS.Queries.Post.GetPostByIdQuery;
using HobbistApi.CQRS.Queries.Post.GetPostsByHashTag;
using HobbistApi.CQRS.Queries.Post.GetPostsByUserIdAndHashTag;
using HobbistApi.CQRS.Queries.Post.GetPostsByUserIdQuery;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Models.Models.DTOs;
using Models.Models.DTOs.Post;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public class PostCQRSController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostCQRSController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        #region GET

        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetPostById(Guid postId)
        {
            if (postId == Guid.Empty) return BadRequest();

            var query = new GetPostByIdQueryRequest() { PostId = postId };
            var reponse = await _mediator.Send(query);

            return reponse != null ? Ok(reponse) : BadRequest();
        }

        [HttpGet("GetPostsByProfileId")]
        public async Task<IActionResult> GetPostsByProfileId(Guid profileId, int index)
        {
            if (profileId == Guid.Empty || index < 0) return BadRequest();

            var query = new GetPostsByProfileIdQueryRequest() { ProfileId = profileId, Index = index };

            var reponse = await _mediator.Send(query);

            return reponse != null ? Ok(reponse) : BadRequest();
        }

        [HttpGet("GetPostsByProfileIdAndHashTag")]
        public async Task<IActionResult> GetPostsByProfileIdAndHashTag(Guid profileId, string hashTagName, int index, Guid requestingProfileId)
        {
            if (profileId == Guid.Empty
                || string.IsNullOrEmpty(hashTagName)
                || index < 0
                || requestingProfileId == Guid.Empty) return BadRequest();

            var query = new GetPostsByProfileIdAndHasTagRequest() 
                { ProfileId = profileId, Index = index, HashTagName = hashTagName, RequestingProfileId = requestingProfileId };

            var reponse = await _mediator.Send(query);

            return reponse != null ? Ok(reponse) : BadRequest();
        }

        [HttpGet("GetBlockedPostList")]
        public async Task<IActionResult> GetBlockedPostList()
        {
            var query = new GetBlockedPostsQueryRequest();

            var response = await _mediator.Send(query);

            return response != null ? Ok(response) : BadRequest();
        }


        [HttpGet("GetPostsByHashTag")]
        public async Task<IActionResult> GetPostsByHashTag(string hashTagName, int index, Guid requestingUserId)
        {
            if (string.IsNullOrEmpty(hashTagName) 
                || index < 0
                || requestingUserId == Guid.Empty) return BadRequest();

            var query = new GetPostsByHashTagRequest() { Index = index, HashTagName = hashTagName, RequestingProfileId = requestingUserId };

            var reponse = await _mediator.Send(query);

            return reponse != null ? Ok(reponse) : BadRequest();
        }

        #endregion GET

        #region CREATE-UPDATE

        [HttpPost("upsertPost")]
        public async Task<IActionResult> UpsertPost([FromBody] PostDto postDto)
        {
            if (postDto == null
                || string.IsNullOrEmpty(postDto.ChainedTagName)
                || string.IsNullOrEmpty(postDto.PostMessage))
                return BadRequest();

            var query = new UpsertPostCommandRequest() { PostDto = postDto };

            return await HandleCommandMediatorAndResponse(query);
        }

        #endregion CREATE-UPDATE

        #region DELETE

        [HttpPost("DeletePost")]
        public async Task<IActionResult> DeletePost([FromBody] PostIdModel postId)
        {
            if (postId.PostId == Guid.Empty) return BadRequest();

            var query = new DeletePostCommandRequest() { PostId = postId.PostId };

            return await HandleCommandMediatorAndResponse(query);
        }

        #endregion DELETE

        #region FOLLOWERS

        [HttpPost("AddFollower")]
        public async Task<IActionResult> AddFollower([FromBody] PostIdFollowerIdModel requestModel)
        {
            if (requestModel.PostId == Guid.Empty || requestModel.FollowerId == Guid.Empty) return BadRequest();

            var query = new AddFollowerCommandRequest() { PostId = requestModel.PostId, FollowerId = requestModel.FollowerId };

            return await HandleCommandMediatorAndResponse(query);
        }

        [HttpPost("RemoveFollower")]
        public async Task<IActionResult> RemoveFollower([FromBody] PostIdFollowerIdModel requestModel)
        {
            if (requestModel.PostId == Guid.Empty || requestModel.FollowerId == Guid.Empty) return BadRequest();

            var query = new RemoveFollowerCommandRequest() { PostId = requestModel.PostId, FollowerId = requestModel.FollowerId };

            return await HandleCommandMediatorAndResponse(query);
        }

        #endregion FOLLOWERS

        #region BLOCK

        [HttpPost("BlockPost")]
        public async Task<IActionResult> BlockPost([FromBody] PostIdModel postId)
        {
            if (postId.PostId == Guid.Empty ) return BadRequest();

            var query = new BlockPostCommandRequest() { PostId = postId.PostId};

            return await HandleCommandMediatorAndResponse(query);
        }

        [HttpPost("UnblockPost")]
        public async Task<IActionResult> UnblockPost([FromBody] PostIdModel postId)
        {
            if (postId.PostId == Guid.Empty) return BadRequest();

            var query = new UnblockPostCommandRequest() { PostId = postId.PostId };

            return await HandleCommandMediatorAndResponse(query);
        }

        #endregion BLOCK

        private async Task<IActionResult> HandleCommandMediatorAndResponse<T>(T query)
            => await CommandHttpResponse((int)await _mediator.Send(query));


        private async Task<IActionResult> CommandHttpResponse(int code)
        {
            if (code == 0 || code == 2) return BadRequest();
            if (code == 1) return Ok();

            return BadRequest();
        }
    }
}
