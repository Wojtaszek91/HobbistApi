using FluentAssertions;
using HobbistApi.CQRS.Commands.AddPostMark;
using HobbistApiTest.MockBuilders;
using Microsoft.Extensions.Logging;
using Models.Models;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Commands.PostMark
{
    public class UpsertPostMarkCommandTest
    {
        private readonly Mock<ILogger<UpserPostMarkCommandHandler>> _loggerMock;
        private readonly CancellationToken _cancellationToken;
        private readonly Guid _postId;
        private readonly Models.Models.Post _dummyPost;
        private readonly Guid _userProfileId;
        private readonly UserProfile _dummyUserProfile;

        public UpsertPostMarkCommandTest()
        {
            _loggerMock = new Mock<ILogger<UpserPostMarkCommandHandler>>();
            _cancellationToken = new CancellationToken();
            _postId = Guid.NewGuid();
            _dummyPost = new Models.Models.Post()
            {
                Id = _postId
            };
            _userProfileId = Guid.NewGuid();
            _dummyUserProfile = new UserProfile()
            {
                Id = _userProfileId
            };
        }

        [Fact]
        public async Task UpsertPostMarkCommandRequest_WithCorrectParameters_ShouldReturnIntOne()
        {
            int dummyMark = 0;

            var handler = new UpserPostMarkCommandHandler(
                PostMarkRepoMockBuilder.GetPostMarkRepo_UpsertMark_ForParamsReturnValue(_dummyUserProfile, _dummyPost, dummyMark, true).Object,
                PostRepoMockBuilder.GetPostRepo_GetPostById_Param(_dummyPost).Object,
                UserProfileRepoMockBuilder.GetUserProfileRepo_GetProfileById_ReturnsParam(_dummyUserProfile).Object,
                _loggerMock.Object);

            var request = new UpsertPostMarkCommandRequest()
            {
                PostId = _postId,
                UserProfileId = _userProfileId,
                Mark = dummyMark
            };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task UpsertPostMarkCommandRequest_WithWrongPostId_ShouldReturnIntZero()
        {
            int dummyMark = 0;

            var handler = new UpserPostMarkCommandHandler(
                PostMarkRepoMockBuilder.GetPostMarkRepo_UpsertMark_ForParamsReturnValue(_dummyUserProfile, _dummyPost, dummyMark, true).Object,
                PostRepoMockBuilder.GetPostRepo_GetPostById_OnAnyIdReturnsNull().Object,
                UserProfileRepoMockBuilder.GetUserProfileRepo_GetProfileById_ReturnsParam(_dummyUserProfile).Object,
                _loggerMock.Object);

            var request = new UpsertPostMarkCommandRequest()
            {
                PostId = _postId,
                UserProfileId = _userProfileId,
                Mark = dummyMark
            };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task UpsertPostMarkCommandRequest_WithWrongUserProfileId_ShouldReturnIntZero()
        {
            int dummyMark = 0;

            var handler = new UpserPostMarkCommandHandler(
                PostMarkRepoMockBuilder.GetPostMarkRepo_UpsertMark_ForParamsReturnValue(_dummyUserProfile, _dummyPost, dummyMark, true).Object,
                PostRepoMockBuilder.GetPostRepo_GetPostById_Param(_dummyPost).Object,
                UserProfileRepoMockBuilder.GetUserProfileRepo_GetProfileById_OnAnyIdReturnsNull().Object,
                _loggerMock.Object);

            var request = new UpsertPostMarkCommandRequest()
            {
                PostId = _postId,
                UserProfileId = _userProfileId,
                Mark = dummyMark
            };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task UpsertPostMarkCommandRequest_IfPostMarkRepoReturnsFalse_ShouldReturnIntZero()
        {
            int dummyMark = 0;

            var handler = new UpserPostMarkCommandHandler(
                PostMarkRepoMockBuilder.GetPostMarkRepo_UpsertMark_ForParamsReturnValue(_dummyUserProfile, _dummyPost, dummyMark, false).Object,
                PostRepoMockBuilder.GetPostRepo_GetPostById_Param(_dummyPost).Object,
                UserProfileRepoMockBuilder.GetUserProfileRepo_GetProfileById_OnAnyIdReturnsNull().Object,
                _loggerMock.Object);

            var request = new UpsertPostMarkCommandRequest()
            {
                PostId = _postId,
                UserProfileId = _userProfileId,
                Mark = dummyMark
            };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task UpsertPostMarkCommandRequest_IfThrowException_ShouldReturnIntTwo()
        {
            int dummyMark = 0;

            var handler = new UpserPostMarkCommandHandler(
                PostMarkRepoMockBuilder.GetPostMarkRepo_UpsertMark_ThrowsException().Object,
                PostRepoMockBuilder.GetPostRepo_GetPostById_Param(_dummyPost).Object,
                UserProfileRepoMockBuilder.GetUserProfileRepo_GetProfileById_OnAnyIdReturnsNull().Object,
                _loggerMock.Object);

            var request = new UpsertPostMarkCommandRequest()
            {
                PostId = _postId,
                UserProfileId = _userProfileId,
                Mark = dummyMark
            };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }


    //[Theory]
    //[ClassData(typeof(CommandRequests))]
    //public void Test1(IRequest request)
    //{
    //}
    //public class CommandRequests : IEnumerable<object[]>
    //{
    //    private readonly List<object[]> _data = new List<object[]>
    //    {
    //        new object[] {new UpsertPostMarkCommandRequest() {
    //            PostId = Guid.NewGuid(),
    //            UserProfileId = Guid.NewGuid(),
    //            Mark = 0}
    //        },

    //    };

    //    public IEnumerator<object[]> GetEnumerator()
    //        => _data.GetEnumerator();

    //    IEnumerator IEnumerable.GetEnumerator()
    //        => GetEnumerator();
    //}
}
