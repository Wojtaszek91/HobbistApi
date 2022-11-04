using FluentAssertions;
using HobbistApi.CQRS.Commands.DeleteProfileCommand;
using HobbistApiTest.MockBuilders;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Post
{
    public class UpsertPostCommandTest
    {
        private readonly CancellationToken _cancellationToken;
        private readonly Mock<ILogger<UpsertPostCommandHandler>> _mockLogger;
        private readonly string _hashTagName;
        private readonly Guid _profileId;
        private readonly UserProfile _userProfile;
        private readonly Models.Models.Post _post;
        private readonly PostDto _postDto;
        private readonly HashTag _hashTag;

        public UpsertPostCommandTest()
        {
            _cancellationToken = new CancellationToken();
            _mockLogger = new Mock<ILogger<UpsertPostCommandHandler>>();

            _hashTagName = "TestHashTagName";
            var postId = Guid.NewGuid();

            _post = new Models.Models.Post()
            {
                Id = postId
            };

            _profileId = Guid.NewGuid();
            _userProfile = new UserProfile()
            {
                Id = _profileId          
            };

            _hashTag = new HashTag()
            {
                HashTagName = _hashTagName,
                Id = Guid.NewGuid(),
                UserProfiles = new List<UserProfile>() { _userProfile }
            };

            _post.ChainedTag = _hashTag;

            _postDto = new PostDto()
            {
                Id = postId,
                ChainedTagName = _hashTagName,
                ProfileId = _profileId
            };

            _userProfile.HashTags = new List<HashTag>() { _hashTag };
        }

        [Fact]
        public async Task UpsertPostCommandRequest_WithCorrectParams_AddPost_ShouldReturnIntOne()
        {
            var handler = new UpsertPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_AddPost_ByParamsReturnValue(false, true).Object,
                HashTagRepoMockBuilder.GetHashTagRepo_GetHashTagByName_WithParamNameReturnsParamDto(_hashTagName, _hashTag).Object,
                UserProfileRepoMockBuilder.GetUserProfileRepo_GetProfileById_ReturnsParam(_userProfile).Object,
                _mockLogger.Object);

            var request = new UpsertPostCommandRequest() { PostDto = _postDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task UpsertPostCommandRequest_WithCorrectParams_UpdatePost_ShouldReturnIntOne()
        {
            var handler = new UpsertPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_EditPost_ByParamsReturnValue(true, true).Object,
                HashTagRepoMockBuilder.GetHashTagRepo_GetHashTagByName_WithParamNameReturnsParamDto(_hashTagName, _hashTag).Object,
                UserProfileRepoMockBuilder.GetUserProfileRepo_GetProfileById_ReturnsParam(_userProfile).Object,
                _mockLogger.Object);

            var request = new UpsertPostCommandRequest() { PostDto = _postDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task UpsertPostCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new UpsertPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_DoesPostExists_ThrowsException().Object,
                HashTagRepoMockBuilder.GetHashTagRepo_GetHashTagByName_WithParamNameReturnsParamDto(_hashTagName, _hashTag).Object,
                UserProfileRepoMockBuilder.GetUserProfileRepo_GetProfileById_ReturnsParam(_userProfile).Object,
                _mockLogger.Object);

            var request = new UpsertPostCommandRequest() { PostDto = _postDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }

        [Fact]
        public async Task UpsertPostCommandRequest_WithWrongHashTagName_ShouldReturnIntTwo()
        {
            var handler = new UpsertPostCommandHandler(
                null,
                HashTagRepoMockBuilder.GetHashTagRepo_GetHashTagByName_WithParamNameReturnsParamDto("", _hashTag).Object,
                UserProfileRepoMockBuilder.GetUserProfileRepo_GetProfileById_ReturnsParam(_userProfile).Object,
                _mockLogger.Object);

            var request = new UpsertPostCommandRequest() { PostDto = _postDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }

        [Fact]
        public async Task UpsertPostCommandRequest_WithWrongProfileId_ShouldReturnIntTwo()
        {
            var dummyProfile = new UserProfile()
            { 
                Id = Guid.NewGuid()
            };

            var handler = new UpsertPostCommandHandler(
                null,
                HashTagRepoMockBuilder.GetHashTagRepo_GetHashTagByName_WithParamNameReturnsParamDto(_hashTagName, _hashTag).Object,
                UserProfileRepoMockBuilder.GetUserProfileRepo_GetProfileById_ReturnsParam(dummyProfile).Object,
                _mockLogger.Object);

            var request = new UpsertPostCommandRequest() { PostDto = _postDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
