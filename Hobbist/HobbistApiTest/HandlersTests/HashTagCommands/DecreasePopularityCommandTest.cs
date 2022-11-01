using FluentAssertions;
using HobbistApi.CQRS.Commands.HashTag.DecreasePopularityCommand;
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
    public class DecreasePopularityCommandTest
    {
        private readonly Mock<ILogger<DecreasePopularityCommandHandler>> _mockLogger;
        private readonly Guid _hashTagId;
        private readonly CancellationToken _cancellationToken;

        public DecreasePopularityCommandTest()
        {
            _hashTagId = Guid.NewGuid();
            _mockLogger = new Mock<ILogger<DecreasePopularityCommandHandler>>();
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async Task DecreasePopularityCommandRequest_WithCorrectParams_ShouldReturnIntOne()
        {
            var handler = new DecreasePopularityCommandHandler(HashTagRepoMockBuilder.GetHashTagRepo_DecreasePopularity_ByParamsReturnValue(
                _hashTagId,
                true).Object, _mockLogger.Object);

            var request = new DecreasePopularityCommandRequest() { HashTagId = _hashTagId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task DecreasePopularityCommandRequest_WithRepoReturnFalse_ShouldReturnIntZero()
        {
            var handler = new DecreasePopularityCommandHandler(HashTagRepoMockBuilder.GetHashTagRepo_DecreasePopularity_ByParamsReturnValue(
                _hashTagId,
                false).Object, _mockLogger.Object);

            var request = new DecreasePopularityCommandRequest() { HashTagId = _hashTagId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task DecreasePopularityCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new DecreasePopularityCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_DecreasePopularity_ThrowsException().Object,
                _mockLogger.Object);

            var request = new DecreasePopularityCommandRequest() { HashTagId = _hashTagId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
