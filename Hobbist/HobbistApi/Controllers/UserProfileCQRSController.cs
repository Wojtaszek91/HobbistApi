using HobbistApi.CQRS.Commands.DeleteProfileCommand;
using HobbistApi.CQRS.Commands.UpdateProfileCommand;
using HobbistApi.CQRS.Commands.UpdateProfileCommand.cs;
using HobbistApi.CQRS.Commands.UpdateProfilePhotoCommand;
using HobbistApi.CQRS.Commands.UserProfile.AddHashtagCommand;
using HobbistApi.CQRS.Commands.UserProfile.RemoveHashtagCommand;
using HobbistApi.CQRS.Queries.GetProfileCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Models.DTOs;
using Models.Models.DTOs.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public class UserProfileCQRSController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserProfileCQRSController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        #region GET

        [HttpGet("GetProfileById")]
        public async Task<IActionResult> GetProfileById(Guid profileId)
        {
            if (profileId == Guid.Empty) return BadRequest();

            var query = new GetProfileByIdQueryRequest() { ProfileId = profileId };

            var response = await _mediator.Send(query);

            return response.UserProfle != null ? Ok(response) : BadRequest();
        }

        #endregion GET

        #region UPDATE

        [HttpPost("updateProfile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateProfile([FromBody] UpsertProfileDto userProfileDto)
        {
            if (userProfileDto == null) return BadRequest();

            var query = new UpdateProfileCommandRequest() { UserProfileDto = userProfileDto };

            return await HandleCommandMediatorAndResponse(query);
        }

        [HttpPost("AddProfilePhoto")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddProfilePhoto([FromBody] AddProfilePhotoDto addPhotoDto)
        {
            if (string.IsNullOrEmpty(addPhotoDto.PhotoBase64) || addPhotoDto.UserProfileId == Guid.Empty) return BadRequest();

            var query = new UpdateProfilePhotoCommandRequest() { AddProfilePhotoDto = addPhotoDto };

            return await HandleCommandMediatorAndResponse(query);
        }

        [HttpPost("UpdateHashtagsProfile")]
        public async Task<IActionResult> UpdateHashtagsProfile([FromBody] HashtagNamesAndProfileId request)
        {
            if (request.HashtagNames == null || request.ProfileId == Guid.Empty) return BadRequest();

            var query = new UpdateProfileHashtagsCommandRequest() { HashtagsList = request.HashtagNames, ProfileId = request.ProfileId };

            return await HandleCommandMediatorAndResponse(query);
        }

        [HttpPost("AddHashtagToProfile")]
        public async Task<IActionResult> AddHashtagToProfile([FromBody] HashtagNameAndProfileId ReqParams)
        {
            if (string.IsNullOrEmpty(ReqParams.HashtagName) || ReqParams.ProfileId == Guid.Empty) return BadRequest();

            var query = new AddHashtagToProfileCommandRequest() { ProfileId = ReqParams.ProfileId, HashtagName = ReqParams.HashtagName };

            return await HandleCommandMediatorAndResponse(query);
        }

        [HttpPost("RemoveHashtagToProfile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RemoveHashtagToProfile([FromBody] HashtagNameAndProfileId ReqParams)
        {
            if (string.IsNullOrEmpty(ReqParams.HashtagName) || ReqParams.ProfileId == Guid.Empty) return BadRequest();

            var query = new RemoveHashtagFromProfileCommandRequest() { ProfileId = ReqParams.ProfileId, HashtagName = ReqParams.HashtagName };

            return await HandleCommandMediatorAndResponse(query);
        }

        #endregion UPDATE

        #region DELETE

        [HttpDelete("{profileId:int}", Name = "DeleteProfile")]
        public async Task<IActionResult> DeleteProfile(Guid profileId)
        {
            if (profileId == Guid.Empty) return BadRequest();

            var query = new DeleteProfileCommandRequest() { ProfileId = profileId };

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
