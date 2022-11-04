using FluentAssertions;
using HobbistApi.CQRS.Commands.UpdateProfilePhotoCommand;
using HobbistApiTest.MockBuilders;
using Microsoft.Extensions.Logging;
using Models.Models.DTOs;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Profile
{
    public class UpdateProfilePhotoCommandTest
    {
        private readonly AddProfilePhotoDto _addProfilePhotoDto;
        private readonly Mock<ILogger<UpdateProfilePhotoCommandHandler>> _mockLogger;
        private readonly CancellationToken _cancellationToken;
        public UpdateProfilePhotoCommandTest()
        {
            _addProfilePhotoDto = new AddProfilePhotoDto()
            {
                PhotoBase64 = "",
                UserProfileId = Guid.NewGuid()
            };
            _mockLogger = new Mock<ILogger<UpdateProfilePhotoCommandHandler>>();
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async Task UpdateProfilePhotoCommandRequest_WithCorrectProfileDto_ShouldReturnIntOne()
        {
            var handler = new UpdateProfilePhotoCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_UpdateProfilePhotoBase64_ForParamReturnParam(
                    _addProfilePhotoDto.PhotoBase64,
                    _addProfilePhotoDto.UserProfileId,
                    true).Object,
                _mockLogger.Object);

            var request = new UpdateProfilePhotoCommandRequest() { AddProfilePhotoDto = _addProfilePhotoDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task UpdateProfilePhotoCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new UpdateProfilePhotoCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_UpdateProfilePhotoBase64_ForParamReturnParam(
                    _addProfilePhotoDto.PhotoBase64,
                    _addProfilePhotoDto.UserProfileId,
                    false).Object,
                _mockLogger.Object);

            var request = new UpdateProfilePhotoCommandRequest() { AddProfilePhotoDto = _addProfilePhotoDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task UpdateProfilePhotoCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new UpdateProfilePhotoCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_UpdateProfilePhotoBase64_ThrowsException().Object,
                _mockLogger.Object);

            var request = new UpdateProfilePhotoCommandRequest() { AddProfilePhotoDto = _addProfilePhotoDto };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
