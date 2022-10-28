//using DAL.Repositories.IRepositories;
//using HobbistApi.Mappings;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Models.Models.DTOs;
//using Models.Models.EntityFrameworkJoinEntities.DTOs;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace HobbistApi.Controllers
//{
//    [Authorize]
//    [Route("api/v{version:apiVersion}/Posts")]
//    [ApiController]
//    public class PostController : ControllerBase
//    {
//        private readonly IPostRepository _postRepo;
//        private readonly IHashTagRepository _hashTagRepo;
//        private readonly IUserProfileRepository _userProfileRepo;

//        public PostController(IPostRepository postRepo, IHashTagRepository hashTagRepo, IUserProfileRepository userProfileRepo)
//        {
//            _postRepo = postRepo;
//            _hashTagRepo = hashTagRepo;
//            _userProfileRepo = userProfileRepo;
//        }

//        #region Gets

//        [HttpGet("GetPostById/{postId:int}")]
//        [ProducesResponseType(200, Type = typeof(PostDto))]
//        [ProducesResponseType(401)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(400)]
//        public IActionResult GetPostById(int postId)
//        {
//            var postFromDb = _postRepo.GetPostById(postId);
//            if (postFromDb == null) return BadRequest();

//            var hashTag = _hashTagRepo.GetHashTagById(postFromDb.ChainedTag.Id);
//            if (hashTag == null) return NotFound();

//            PostDto postDto = new PostDto()
//            {
//                ChainedTagName = hashTag.HashTagName,
//                PostMessage = postFromDb.PostMessage,
//                PostViews = postFromDb.PostViews,
//                AverageMark = postFromDb.AverageMark,
//                DayLast = postFromDb.DayLast,
//                BeginDate = postFromDb.BeginDate,
//                ProfileId = postFromDb.UserProfileId
//            };
//            return Ok(postDto);
//        }

//        [HttpGet("GetPostsByUserId/{userId:int}/{index:int}")]
//        [ProducesResponseType(200, Type = typeof(IQueryable<PostDto>))]
//        [ProducesResponseType(401)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(400)]
//        public IActionResult GetPostsByUserId(int userId, int index)
//        {
//            var postCollection = _postRepo.GetPostsByUserId(userId, index);
//            if (postCollection.Count() <= 0) return BadRequest();

//            var postDtoCollection = PostMapper.MapCollectionPostToPostDto(postCollection, userId);

//            return Ok(new PostsListWithIndexDto() { PostList = postDtoCollection, Index = index });
//        }

//        [HttpGet("GetPostsByUserIdAndHashTag/{userId:int}/{hashTagName}/{index:int}")]
//        [ProducesResponseType(200, Type = typeof(IQueryable<PostDto>))]
//        [ProducesResponseType(401)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(400)]
//        public IActionResult GetPostsByUserIdAndHashTag(int userId, string hashTagName, int index, int requestinUserId)
//        {
//            if (userId <= 0
//                || string.IsNullOrEmpty(hashTagName)
//                || index <= 0
//                || requestinUserId <= 0) return BadRequest();

//            var postCollection = _postRepo.GetPostsByUserIdAndHashTag(userId, hashTagName, index);
//            var postDtoCollection = PostMapper.MapCollectionPostToPostDto(postCollection, requestinUserId);

//            return Ok(new PostsListWithIndexDto() { PostList = postDtoCollection, Index = index });
//        }

//        [HttpGet("GetPostsByHashTag/{hashTagName}/{index}/{requestingUserId}")]
//        [ProducesResponseType(200, Type = typeof(PostsListWithIndexDto))]
//        [ProducesResponseType(401)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(400)]
//        public IActionResult GetPostsByHashTag(string hashTagName, int index, int requestingUserId)
//        {
//            if (string.IsNullOrEmpty(hashTagName) || index >= 0) return BadRequest();

//            var postCollection = _postRepo.GetPostsByHashTag(hashTagName, index);
//            var postDtoCollection = PostMapper.MapCollectionPostToPostDto(postCollection, requestingUserId);

//            return Ok(new PostsListWithIndexDto() { PostList = postDtoCollection, Index = index });
//        }

//        #endregion Gets

//        [HttpPost("upsertPost")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(401)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(400)]
//        public IActionResult UpsertPost([FromBody] PostDto postDto)
//        {
//            if (postDto == null
//                || string.IsNullOrEmpty(postDto.ChainedTagName)
//                || string.IsNullOrEmpty(postDto.PostMessage))
//                return BadRequest();

//            var hashTag = _hashTagRepo.GetHashTagByName(postDto.ChainedTagName);
//            var userProfile = _userProfileRepo.GetProfileById(postDto.ProfileId);
//            if (hashTag == null && userProfile == null) return BadRequest();

//            var postToDb = PostMapper.MapPostDtoToPost(postDto, userProfile, hashTag);

//            if (!_postRepo.DoesPostExists(postToDb.Id)) { return _postRepo.AddPost(postToDb) ? Ok() : BadRequest(); }
//            else { return _postRepo.EditPost(postToDb) ? Ok() : BadRequest(); }
//        }

//        [HttpPost("DeletePost")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(400)]
//        public IActionResult DeletePost([FromBody] PostIdModel postId) => _postRepo.DeletePost(postId.PostId) ? Ok(postId) : BadRequest();

//        [HttpPost("AddFollower/{postId:int}/{followerId:int}")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(400)]
//        public IActionResult AddFollower(int postId, int followerId) => _postRepo.AddFollower(postId, followerId) ? Ok() : BadRequest();


//        [HttpPost("RemoveFollower/{postId:int}/{followerId:int}")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(400)]
//        public IActionResult RemoveFollower(int postId, int followerId) => _postRepo.RemoveFollower(postId, followerId) ? Ok() : BadRequest();

//        [HttpPost("BlockPost")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(400)]
//        public IActionResult BlockPost([FromBody] PostIdModel postId) => _postRepo.BlockPost(postId.PostId) ? NoContent() : BadRequest();


//        [HttpPost("UnblockPost")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(400)]
//        public IActionResult UnblockPost([FromBody] PostIdModel postId) => _postRepo.UnblockPost(postId.PostId) ? NoContent() : BadRequest();

//    }
//}

