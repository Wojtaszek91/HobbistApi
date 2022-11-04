using FluentAssertions;
using HobbistApi.CQRS.Commands.HashTag.UpdateHashTagCommand;
using HobbistApiTest.MockBuilders;
using Microsoft.Extensions.Logging;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Commands.HashTag
{
    public class UpdateHashTagCommandTest
    {
        private readonly Mock<ILogger<UpdateHashTagCommandHandler>> _mockLogger;
        private readonly HashTagDto _hashTagDto;
        private readonly CancellationToken _cancellationToken;

        public UpdateHashTagCommandTest()
        {
            _mockLogger = new Mock<ILogger<UpdateHashTagCommandHandler>>();
            _cancellationToken = new CancellationToken();
            _hashTagDto = new HashTagDto()
            {
                HashTagName = "TestName",
                Popularity = 1
            };
        }

        [Fact]
        public async Task DeleteHashTagCommandRequest_WithCorrectHashTagName_ShouldReturnIntOne()
        {
            var handler = new UpdateHashTagCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_UpdateHashTag_ByParamsReturnValue(_hashTagDto, true).Object,
                _mockLogger.Object);

            var request = new UpdateHashTagCommandRequest() { HashTagDto = _hashTagDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task DeleteHashTagCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new UpdateHashTagCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_UpdateHashTag_ByParamsReturnValue(_hashTagDto, false).Object,
                _mockLogger.Object);

            var request = new UpdateHashTagCommandRequest() { HashTagDto = _hashTagDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task DeleteHashTagCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new UpdateHashTagCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_UpdateHashTag_ThrowsException().Object,
                _mockLogger.Object);

            var request = new UpdateHashTagCommandRequest() { HashTagDto = _hashTagDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
