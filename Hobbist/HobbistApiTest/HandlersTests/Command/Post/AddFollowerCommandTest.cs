using FluentAssertions;
using HobbistApi.CQRS.Commands.Post.AddFollower;
using HobbistApiTest.MockBuilders;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Post
{
    public class AddFollowerCommandTest
    {
        private readonly Mock<ILogger<AddFollowerCommandHandler>> _mockLogger;
        private readonly Guid _postId;
        private readonly Guid _followerId;
        private readonly CancellationToken _cancellationToken;

        public AddFollowerCommandTest()
        {
            _mockLogger = new Mock<ILogger<AddFollowerCommandHandler>>();
            _cancellationToken = new CancellationToken();
            _postId = Guid.NewGuid();
            _followerId = Guid.NewGuid();
        }

        [Fact]
        public async Task AddFollowerCommandRequest_WithCorrectHashTagName_ShouldReturnIntOne()
        {
            var handler = new AddFollowerCommandHandler(
                PostRepoMockBuilder.GetPostRepo_AddFollower_ByParamsReturnValue(_postId, _followerId, true).Object,
                _mockLogger.Object);

            var request = new AddFollowerCommandRequest() { PostId = _postId, FollowerId = _followerId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task AddFollowerCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new AddFollowerCommandHandler(
                PostRepoMockBuilder.GetPostRepo_AddFollower_ByParamsReturnValue(_postId, _followerId, false).Object,
                _mockLogger.Object);

            var request = new AddFollowerCommandRequest() { PostId = _postId, FollowerId = _followerId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task AddFollowerCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new AddFollowerCommandHandler(
                PostRepoMockBuilder.GetPostRepo_AddFollower_ThrowsException().Object,
                _mockLogger.Object);

            var request = new AddFollowerCommandRequest() { PostId = _postId, FollowerId = _followerId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
