using DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Models.Models.DTOs;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System.Collections.Generic;

namespace HobbistApi.Controllers
{
    //[Authorize]
    [Route("api/v{version:apiVersion}/HashTags")]
    [ApiController]
    public class HashTagController : ControllerBase
    {
        private readonly IHashTagRepository _hashTagRepo;

        public HashTagController(IHashTagRepository hashTagRepo)
        {
            _hashTagRepo = hashTagRepo;
        }

        //[HttpGet("GetAllHashtags")]
        //[ProducesResponseType(200, Type = typeof(List<HashTagDto>))]
        //public IActionResult GetAllHashtagsDto()
        //{       
        //    return Ok(new HashTagList() { HashTagDtoList = _hashTagRepo.GetAllHashtagsDto() });
        //}

        //[HttpGet("GetHashTagById/{hashTagId:int}")]
        //[ProducesResponseType(200, Type = typeof(HashTagDto))]
        //[ProducesResponseType(401)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public IActionResult GetHashTagById(int hashTagId)
        //{
        //    if (hashTagId <= 0) return BadRequest();
        //    var hashTag = _hashTagRepo.GetHashTagById(hashTagId);
        //    return hashTag != null ? Ok(hashTag) : BadRequest();
        //}

        //[HttpGet("AddPopularity/{hashTagId}")]
        //public IActionResult AddPopularity(int hashTagId)
        //{
        //    if (_hashTagRepo.AddPopularity(hashTagId)){return Ok();}
        //    return BadRequest();
        //}

        //[HttpGet("DecreasePopularity/{hashTagId}")]
        //public IActionResult DecreasePopularity(int hashTagId)
        //{
        //    if (_hashTagRepo.DecreasePopuplarity(hashTagId)){return Ok();}
        //    return BadRequest();
        //}

        //[HttpPost("CreateHashtag")]
        //public IActionResult CreateHashTag([FromBody] AddHashtagNameDto hashTag)
        //{
        //    if (string.IsNullOrEmpty(hashTag.HashtagName)) return BadRequest();
        //    return _hashTagRepo.AddHashTag(hashTag.HashtagName) ? Ok(hashTag) : BadRequest();
        //}

        //[HttpPatch("UpdateHashtag")]
        //public IActionResult UpdateHashTag([FromBody] HashTagDto hashTag)
        //{
        //    if (hashTag == null) return BadRequest();
        //    return _hashTagRepo.EditHashTagNoReturnType(hashTag) ? Ok() : BadRequest();
        //}

        //[HttpDelete("{hashTagId:int}")]
        //public IActionResult DeleteHashTag(int hashTagId)
        //{
        //    if (!_hashTagRepo.DoesHashTagExists(hashTagId)){return NotFound();}

        //    if (!_hashTagRepo.DeleteHashTag(hashTagId))
        //    {
        //        ModelState.AddModelError("", $"Something went wrong when deleting the profile {hashTagId}");
        //        return StatusCode(500, ModelState);
        //    }

        //    return NoContent();
        //}
    }
}
