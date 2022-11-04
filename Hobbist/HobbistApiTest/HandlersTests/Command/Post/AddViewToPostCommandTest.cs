using FluentAssertions;
using HobbistApi.CQRS.Commands.Post.AddViewPost;
using HobbistApiTest.MockBuilders;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Post
{
    public class AddViewToPostCommandTest
    {
        private readonly Mock<ILogger<AddViewToPostCommandHandler>> _mockLogger;
        private readonly Guid _postId;
        private readonly CancellationToken _cancellationToken;

        public AddViewToPostCommandTest()
        {
            _mockLogger = new Mock<ILogger<AddViewToPostCommandHandler>>();
            _cancellationToken = new CancellationToken();
            _postId = Guid.NewGuid();
        }

        [Fact]
        public async Task AddViewToPostCommandRequest_WithCorrectHashTagName_ShouldReturnIntOne()
        {
            var handler = new AddViewToPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_AddPostView_ByParamsReturnValue(_postId, true).Object,
                _mockLogger.Object);

            var request = new AddViewToPostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task AddViewToPostCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new AddViewToPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_AddPostView_ByParamsReturnValue(_postId, false).Object,
                _mockLogger.Object);

            var request = new AddViewToPostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task AddViewToPostCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new AddViewToPostCommandHandler(
                PostRepoMockBuilder.GetPostRepo_AddPostView_ThrowsException().Object,
                _mockLogger.Object);

            var request = new AddViewToPostCommandRequest() { PostId = _postId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
