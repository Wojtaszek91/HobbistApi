using FluentAssertions;
using HobbistApi.CQRS.Commands.Post.RemoveFollower;
using HobbistApiTest.MockBuilders;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Post
{
    public class RemoveFollowerCommandTest
    {
        private readonly Mock<ILogger<RemoveFollowerCommandHandler>> _mockLogger;
        private readonly Guid _postId;
        private readonly Guid _followerId;
        private readonly CancellationToken _cancellationToken;

        public RemoveFollowerCommandTest()
        {
            _mockLogger = new Mock<ILogger<RemoveFollowerCommandHandler>>();
            _cancellationToken = new CancellationToken();
            _postId = Guid.NewGuid();
            _followerId = Guid.NewGuid();
        }

        [Fact]
        public async Task RemoveFollowerCommandRequest_WithCorrectHashTagName_ShouldReturnIntOne()
        {
            var handler = new RemoveFollowerCommandHandler(
                PostRepoMockBuilder.GetPostRepo_RemoveFollower_ByParamsReturnValue(_postId, _followerId, true).Object,
                _mockLogger.Object);

            var request = new RemoveFollowerCommandRequest() { PostId = _postId, FollowerId = _followerId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task RemoveFollowerCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new RemoveFollowerCommandHandler(
                PostRepoMockBuilder.GetPostRepo_RemoveFollower_ByParamsReturnValue(_postId, _followerId, false).Object,
                _mockLogger.Object);

            var request = new RemoveFollowerCommandRequest() { PostId = _postId, FollowerId = _followerId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task RemoveFollowerCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new RemoveFollowerCommandHandler(
                PostRepoMockBuilder.GetPostRepo_RemoveFollower_ThrowsException().Object,
                _mockLogger.Object);

            var request = new RemoveFollowerCommandRequest() { PostId = _postId, FollowerId = _followerId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
