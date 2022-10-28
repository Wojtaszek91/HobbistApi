using DAL.Repositories.IRepositories;
using HobbistApi.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Models.DTOs;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.Controllers
{
    ///[Authorize]
    [Route("api/v{version:apiVersion}/profile")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _profileRepo;

        public UserProfileController(IUserProfileRepository profileRepo)
        {
            _profileRepo = profileRepo;
        }

        //    [HttpGet("GetProfileById/{id:int}")]
        //    [ProducesResponseType(200, Type = typeof(UserProfile))]
        //    [ProducesResponseType(401)]
        //    [ProducesResponseType(404)]
        //    [ProducesResponseType(400)]
        //    public IActionResult GetProfileById(int id)
        //    {
        //        var profile = _profileRepo.GetProfileById(id);
        //        if (profile == null) return BadRequest();            
        //        return Ok(profile);         
        //    }

        //    [HttpPost("updateProfile")]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(400)]
        //    public IActionResult UpdateProfile([FromBody] UserProfileDto userProfileDto, string username)
        //    {
        //        if (userProfileDto == null || string.IsNullOrEmpty(username)) return BadRequest();

        //        return _profileRepo.UpdateProfile(userProfileDto) ? Ok() : BadRequest();
        //    }

        //    [HttpPost("UpdateProfilePhoto")]
        //    [ProducesResponseType(200)]
        //    [ProducesResponseType(400)]
        //    public IActionResult UpdateProfilePhoto([FromBody] AddProfilePhotoDto addPhotoDto)
        //    {
        //        if (string.IsNullOrEmpty(addPhotoDto.PhotoBase64) || addPhotoDto.UserProfileId == 0) return BadRequest();
        //        return _profileRepo.UpdateProfilePhotoBase64(addPhotoDto.PhotoBase64, addPhotoDto.UserProfileId) ? Ok() : BadRequest();
        //    }

        //    [HttpDelete("{profileId:int}", Name = "DeleteProfile")]
        //    public IActionResult DeleteProfile(int profileId)
        //    {
        //        if (!_profileRepo.DoesProfileExist(profileId))
        //        {
        //            return NotFound();
        //        }

        //        if (!_profileRepo.DeleteProfile(profileId))
        //        {
        //            ModelState.AddModelError("", $"Something went wrong when deleting the profile {profileId}");
        //            return StatusCode(500, ModelState);
        //        }

        //        return NoContent();
        //    }
    }
}
