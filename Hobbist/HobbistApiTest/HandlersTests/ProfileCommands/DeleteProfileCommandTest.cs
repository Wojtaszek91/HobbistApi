using FluentAssertions;
using HobbistApi.CQRS.Commands.DeleteProfileCommand;
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

namespace HobbistApiTest.HandlersTests.Commands
{
    public class DeleteProfileCommandTest
    {
        private readonly Guid _profileId;
        private readonly Mock<ILogger<DeleteProfileCommandHandler>> _mockLogger;
        private readonly CancellationToken _cancellationToken;
        public DeleteProfileCommandTest()
        {
            _profileId = Guid.NewGuid();
            _mockLogger = new Mock<ILogger<DeleteProfileCommandHandler>>();
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async Task DeleteProfileCommandRequest_WithCorrectProfileId_ShouldReturnIntOne()
        {
            var handler = new DeleteProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_DoesProfileExistTrue_DeleteProfileReturnParam(_profileId, true).Object,
                _mockLogger.Object);

            var request = new DeleteProfileCommandRequest() { ProfileId = _profileId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task DeleteProfileCommandRequest_WithDoesProfileExistFalseRepoReturn_ShouldReturnIntTwo()
        {
            var handler = new DeleteProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_DoesProfileExist_ForParamReturnParam(_profileId, false).Object,
                _mockLogger.Object);

            var request = new DeleteProfileCommandRequest() { ProfileId = _profileId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }

        [Fact]
        public async Task DeleteProfileCommandRequest_WithDeleteProfileFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new DeleteProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_DoesProfileExistTrue_DeleteProfileReturnParam(_profileId, false).Object,
                _mockLogger.Object);

            var request = new DeleteProfileCommandRequest() { ProfileId = _profileId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task DeleteProfileCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new DeleteProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_DoesProfileExist_ThrowsException().Object,
                _mockLogger.Object);

            var request = new DeleteProfileCommandRequest() { ProfileId = _profileId };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
