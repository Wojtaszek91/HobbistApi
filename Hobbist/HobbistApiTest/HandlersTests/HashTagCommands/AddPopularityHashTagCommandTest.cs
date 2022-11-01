using FluentAssertions;
using HobbistApi.CQRS.Commands.HashTag.AddPopularityCommand;
using HobbistApi.CQRS.Commands.HashTag.CreateHashTagCommand;
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
    public class AddPopularityHashTagCommandTest
    {
        private readonly Guid _hashTagId;
        private readonly Mock<ILogger<AddPopularityCommandHandler>> _mockLogger;
        private readonly CancellationToken _cancellationToken;
        public AddPopularityHashTagCommandTest()
        {
            _hashTagId = Guid.NewGuid();
            _mockLogger = new Mock<ILogger<AddPopularityCommandHandler>>();
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async Task AddPopularityCommandRequest_WithCorrectHashTagId_ShouldReturnIntOne()
        {
            var handler = new AddPopularityCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_AddPopularity_ByParamsReturnValue(_hashTagId, true).Object,
                _mockLogger.Object);

            var request = new AddPopularityCommandRequest() { HashTagId = _hashTagId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task AddPopularityCommandRequest_WithIncorrectHashTagId_ShouldReturnIntZero()
        {
            var handler = new AddPopularityCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_AddPopularity_ByParamsReturnValue(_hashTagId, false).Object,
                _mockLogger.Object);

            var request = new AddPopularityCommandRequest() { HashTagId = _hashTagId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task AddPopularityCommandRequest_WithExceptionThrown_ShouldReturnIntTwo()
        {
            var handler = new AddPopularityCommandHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_AddPopularity_ThrowsException().Object,
                _mockLogger.Object);

            var request = new AddPopularityCommandRequest() { HashTagId = _hashTagId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
