using HobbistApi.CQRS.Commands.HashTag.AddPopularityCommand;
using HobbistApi.CQRS.Commands.HashTag.CreateHashTagCommand;
using HobbistApi.CQRS.Commands.HashTag.DecreasePopularityCommand;
using HobbistApi.CQRS.Commands.HashTag.DeleteHashTagCommand;
using HobbistApi.CQRS.Commands.HashTag.GetHashTagById;
using HobbistApi.CQRS.Commands.HashTag.UpdateHashTagCommand;
using HobbistApi.CQRS.Queries.HashTag.GetAllHashTagDtoQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Models.DTOs;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashTagCQRSController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HashTagCQRSController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        #region GET

        [HttpGet("GetAllHashtagsDto")]
        public async Task<IActionResult> GetAllHashtagsDto()
        {
            var query = new GetAllHashTagDtoQueryRequest();

            return Ok(await _mediator.Send(query));
        }

        [HttpGet("GetHashTagById")]
        public async Task<IActionResult> GetHashTagById(Guid hashTagId)
        {
            if (hashTagId == Guid.Empty) return BadRequest();
            var query = new GetHashTagByIdQueryRequest() { HashTagId = hashTagId};

            var reponse = await _mediator.Send(query);

            return reponse != null ? Ok(reponse) : BadRequest();
        }

        [HttpGet("GetHashtagsNameList")]
        public async Task<IActionResult> GetHashtagsNameList()
        {
            var query = new GetAllHashTagNameQueryRequest();

            var reponse = await _mediator.Send(query);

            return reponse != null ? Ok(reponse) : BadRequest();
        }

        #endregion GET

        #region CREATE

        [HttpPost("CreateHashtag")]
        public async Task<IActionResult> CreateHashTag([FromBody] HashTagNameDto newHashTagRequest)
        {
            if (string.IsNullOrEmpty(newHashTagRequest.HashTagName)) return BadRequest();

            var query = new CreateHashTagCommandRequest() { NewHashTagName = newHashTagRequest.HashTagName };

            return await HandleCommandMediatorAndResponse(query);
        }

        #endregion CREATE

        #region UPDATE

        [HttpGet("AddPopularity/{hashTagId}")]
        public async Task<IActionResult> AddPopularity(Guid hashTagId)
        {
            if (hashTagId == Guid.Empty) return BadRequest();

            var query = new AddPopularityCommandRequest() { HashTagId = hashTagId};

            return await HandleCommandMediatorAndResponse(query);
        }

        [HttpGet("DecreasePopularity/{hashTagId}")]
        public async Task<IActionResult> DecreasePopularity(Guid hashTagId)
        {
            if (hashTagId == Guid.Empty) return BadRequest();

            var query = new DecreasePopularityCommandRequest() { HashTagId = hashTagId };

            return await HandleCommandMediatorAndResponse(query);
        }

        [HttpPost("UpdateHashtag")]
        public async Task<IActionResult> UpdateHashTag([FromBody] HashTagDto hashTag)
        {
            if (hashTag == null || string.IsNullOrEmpty(hashTag.HashTagName)) return BadRequest();

            var query = new UpdateHashTagCommandRequest() { HashTagDto = hashTag };

            return await HandleCommandMediatorAndResponse(query);
        }

        #endregion UPDATE

        #region DELETE

        [HttpDelete("DeleteHashTag")]
        public async Task<IActionResult> DeleteHashTag(Guid hashTagId)
        {
            if (hashTagId == Guid.Empty) return BadRequest();

            var query = new DeleteHashTagCommandRequest() { HashTagId = hashTagId };

            return await HandleCommandMediatorAndResponse(query);
        }

        #endregion DELETE

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
