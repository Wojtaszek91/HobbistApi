using DAL.Repositories.IRepositories;
using HobbistApi.CQRS.Commands.AddPostMark;
using HobbistApi.CQRS.Queries.PostMark.GetAveragePostMark;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Models.DTOs.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostMarkController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostMarkController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("UpsertMark")]
        public async Task<IActionResult> UpsertMark(UpsertPostMarkModel request)
        {       
            var query = new UpsertPostMarkCommandRequest() {
                PostId = request.PostId, 
                UserProfileId = request.UserProfileId, 
                Mark = request.Mark};

            return await HandleCommandMediatorAndResponse(query);
        }

        [HttpGet("GetPostMark")]
        public async Task<IActionResult> GetPostMark(Guid postId)
        {
            var query = new GetAveragePostMarkQueryRequest() { PostId = postId };
            var reponse = await _mediator.Send(query);

            return reponse != null ? Ok(reponse) : BadRequest();
        }

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
