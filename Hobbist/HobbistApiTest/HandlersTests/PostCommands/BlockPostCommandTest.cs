using FluentAssertions;
using HobbistApi.CQRS.Commands.Post.BlockPost;
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
    public class BlockPostCommandTest
    {
        private readonly Mock<ILogger<BlockPostCommandHandler>> _mockLogger;
        private readonly Guid _postId;
        private readonly CancellationToken _cancellationToken;

        public BlockPostCommandTest()
        {
            _mockLogger = new Mock<ILogger<BlockPostCommandHandler>>();
            _cancellationToken = new CancellationToken();
            _postId = Guid.NewGuid();        }

        [Fact]
        public async Task BlockPostCommandRequest_WithCorrectHashTagName_ShouldReturnIntOne()
        {
            var handler = new BlockPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_BlockPost_ByParamsReturnValue(_postId, true).Object,
                _mockLogger.Object);

            var request = new BlockPostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task BlockPostCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new BlockPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_BlockPost_ByParamsReturnValue(_postId, false).Object,
                _mockLogger.Object);

            var request = new BlockPostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task BlockPostCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new BlockPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_BlockPost_ThrowsException().Object,
                _mockLogger.Object);

            var request = new BlockPostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
