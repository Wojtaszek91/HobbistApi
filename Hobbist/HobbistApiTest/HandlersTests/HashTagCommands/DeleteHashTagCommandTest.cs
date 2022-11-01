using FluentAssertions;
using HobbistApi.CQRS.Commands.HashTag.DeleteHashTagCommand;
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

namespace HobbistApiTest.HandlersTests
{
    public class DeleteHashTagCommandTest
    {
        private readonly Mock<ILogger<DeleteHashTagCommandHandler>> _mockLogger;
        private readonly Guid _hashTagId;
        private readonly CancellationToken _cancellationToken;

        public DeleteHashTagCommandTest()
        {
            _mockLogger = new Mock<ILogger<DeleteHashTagCommandHandler>>();
            _cancellationToken = new CancellationToken();
            _hashTagId = Guid.NewGuid();
        }

        [Fact]
        public async Task DeleteHashTagCommandRequest_WithCorrectHashTagName_ShouldReturnIntOne()
        {
            var handler = new DeleteHashTagCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_DeleteHashTag_ByParamsReturnValue(_hashTagId, true).Object,
                _mockLogger.Object);

            var request = new DeleteHashTagCommandRequest() { HashTagId = _hashTagId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task DeleteHashTagCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new DeleteHashTagCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_DeleteHashTag_ByParamsReturnValue(_hashTagId, false).Object,
                _mockLogger.Object);

            var request = new DeleteHashTagCommandRequest() { HashTagId = _hashTagId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task DeleteHashTagCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new DeleteHashTagCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_DeleteHashTag_ThrowsException().Object,
                _mockLogger.Object);

            var request = new DeleteHashTagCommandRequest() { HashTagId = _hashTagId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
        
    }
}
