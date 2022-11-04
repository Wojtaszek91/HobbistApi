using FluentAssertions;
using HobbistApi.CQRS.Commands.UpdateProfileCommand.cs;
using HobbistApiTest.MockBuilders;
using Microsoft.Extensions.Logging;
using Models.Models.DTOs.Profile;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Profile
{
    public class UpdateProfileCommandTest
    {
        private readonly UpsertProfileDto _upsertProfileDto;
        private readonly Mock<ILogger<UpdateProfileCommandHandler>> _mockLogger;
        private readonly CancellationToken _cancellationToken;
        public UpdateProfileCommandTest()
        {
            _upsertProfileDto = new UpsertProfileDto();
            _mockLogger = new Mock<ILogger<UpdateProfileCommandHandler>>();
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async Task UpdateProfileCommandRequest_WithCorrectProfileDto_ShouldReturnIntOne()
        {
            var handler = new UpdateProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_UpdateProfile_OnParamReturnParam(_upsertProfileDto, true).Object,
                _mockLogger.Object);

            var request = new UpdateProfileCommandRequest() { UserProfileDto = _upsertProfileDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task UpdateProfileCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new UpdateProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_UpdateProfile_OnParamReturnParam(_upsertProfileDto, false).Object,
                _mockLogger.Object);

            var request = new UpdateProfileCommandRequest() { UserProfileDto = _upsertProfileDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task UpdateProfileCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new UpdateProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_UpdateProfile_ThrowsException().Object,
                _mockLogger.Object);

            var request = new UpdateProfileCommandRequest() { UserProfileDto = _upsertProfileDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
