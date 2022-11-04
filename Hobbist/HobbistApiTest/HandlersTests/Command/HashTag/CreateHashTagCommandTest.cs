using FluentAssertions;
using HobbistApi.CQRS.Commands.HashTag.CreateHashTagCommand;
using HobbistApiTest.MockBuilders;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Commands.HashTag
{
    public class CreateHashTagCommandTest
    {
        private readonly string _hashTagName;
        private readonly Mock<ILogger<CreateHashTagCommandHandler>> _mockLogger;
        private readonly CancellationToken _cancellationToken;
        public CreateHashTagCommandTest()
        {
            _hashTagName = "testName";
            _mockLogger = new Mock<ILogger<CreateHashTagCommandHandler>>();
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async Task CreateHashTagCommandRequest_WithCorrectHashTagName_ShouldReturnIntOne()
        {
            var handler = new CreateHashTagCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_AddHashTag_ByParamsReturnValue(_hashTagName, true).Object,
                _mockLogger.Object);

            var request = new CreateHashTagCommandRequest() { NewHashTagName = _hashTagName };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task CreateHashTagCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new CreateHashTagCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_AddHashTag_ByParamsReturnValue(_hashTagName, false).Object,
                _mockLogger.Object);

            var request = new CreateHashTagCommandRequest() { NewHashTagName = _hashTagName };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task CreateHashTagCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new CreateHashTagCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_AddHashTag_ThrowsException().Object,
                _mockLogger.Object);

            var request = new CreateHashTagCommandRequest() { NewHashTagName = _hashTagName };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
