using FluentAssertions;
using HobbistApi.CQRS.Commands.Post.DeletePost;
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
    public class DeletePostCommandTest
    {
        private readonly Mock<ILogger<DeletePostCommandHandler>> _mockLogger;
        private readonly Guid _postId;
        private readonly CancellationToken _cancellationToken;

        public DeletePostCommandTest()
        {
            _mockLogger = new Mock<ILogger<DeletePostCommandHandler>>();
            _cancellationToken = new CancellationToken();
            _postId = Guid.NewGuid();
        }

        [Fact]
        public async Task DeletePostCommandRequest_WithCorrectHashTagName_ShouldReturnIntOne()
        {
            var handler = new DeletePostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_DeletePost_ByParamsReturnValue(_postId, true).Object,
                _mockLogger.Object);

            var request = new DeletePostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task DeletePostCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new DeletePostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_DeletePost_ByParamsReturnValue(_postId, false).Object,
                _mockLogger.Object);

            var request = new DeletePostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task DeletePostCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new DeletePostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_DeletePost_ThrowsException().Object,
                _mockLogger.Object);

            var request = new DeletePostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
