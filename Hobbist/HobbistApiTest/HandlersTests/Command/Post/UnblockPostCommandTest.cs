using FluentAssertions;
using HobbistApi.CQRS.Commands.Post.UnblockPost;
using HobbistApiTest.MockBuilders;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Post
{
    public class UnblockPostCommandTest
    {
        private readonly Mock<ILogger<UnblockPostCommandHandler>> _mockLogger;
        private readonly Guid _postId;
        private readonly CancellationToken _cancellationToken;

        public UnblockPostCommandTest()
        {
            _mockLogger = new Mock<ILogger<UnblockPostCommandHandler>>();
            _cancellationToken = new CancellationToken();
            _postId = Guid.NewGuid();
        }

        [Fact]
        public async Task UnblockPostCommandRequest_WithCorrectHashTagName_ShouldReturnIntOne()
        {
            var handler = new UnblockPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_UnblockPost_ByParamsReturnValue(_postId, true).Object,
                _mockLogger.Object);

            var request = new UnblockPostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task UnblockPostCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new UnblockPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_UnblockPost_ByParamsReturnValue(_postId, false).Object,
                _mockLogger.Object);

            var request = new UnblockPostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task UnblockPostCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new UnblockPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_UnblockPost_ThrowsException().Object,
                _mockLogger.Object);

            var request = new UnblockPostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
