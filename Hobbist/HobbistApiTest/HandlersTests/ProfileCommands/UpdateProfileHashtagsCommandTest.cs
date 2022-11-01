using FluentAssertions;
using HobbistApi.CQRS.Commands.UpdateProfileCommand.cs;
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

namespace HobbistApiTest.HandlersTests.ProfileCommands
{
    public class UpdateProfileHashtagsCommandTest
    {
        private readonly Guid _profileId;
        private readonly List<string> _hashTagList;
        private readonly Mock<ILogger<UpdateProfileHashtagsCommandHandler>> _mockLogger;
        private readonly CancellationToken _cancellationToken;
        public UpdateProfileHashtagsCommandTest()
        {
            _profileId = Guid.NewGuid();
            _mockLogger = new Mock<ILogger<UpdateProfileHashtagsCommandHandler>>();
            _cancellationToken = new CancellationToken();
            _hashTagList = new List<string>()
            {
                "TestHashTagName",
                "AnotherOne",
                "OneMore"
            };
        }

        [Fact]
        public async Task UpdateProfileHashtagsCommandRequest_WithCorrectProfileDto_ShouldReturnIntOne()
        {
            var handler = new UpdateProfileHashtagsCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_DoesProfileExistTrue_UpdateProfileHashtagsByListForParamReturnParam(
                    _profileId,
                    _hashTagList,
                    true).Object,
                _mockLogger.Object);

            var request = new UpdateProfileHashtagsCommandRequest() { ProfileId = _profileId, HashtagsList = _hashTagList };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task UpdateProfileHashtagsCommandRequest_WithFalseRepoReturn_ShouldReturnIntZero()
        {
            var handler = new UpdateProfileHashtagsCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_DoesProfileExistTrue_UpdateProfileHashtagsByListForParamReturnParam(
                    _profileId,
                    _hashTagList,
                    false).Object,
                _mockLogger.Object);

            var request = new UpdateProfileHashtagsCommandRequest() { ProfileId = _profileId, HashtagsList = _hashTagList };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }

        [Fact]
        public async Task UpdateProfileHashtagsCommandRequest_DoesProfileExistReturnFalse_ShouldReturnIntTwo()
        {
            var handler = new UpdateProfileHashtagsCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_DoesProfileExist_ForParamReturnParam(_profileId, false).Object,
                _mockLogger.Object);

            var request = new UpdateProfileHashtagsCommandRequest() { ProfileId = _profileId, HashtagsList = _hashTagList };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }

        [Fact]
        public async Task UpdateProfileHashtagsCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new UpdateProfileHashtagsCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_DoesProfileExist_ThrowsException().Object,
                _mockLogger.Object);

            var request = new UpdateProfileHashtagsCommandRequest() { ProfileId = _profileId, HashtagsList = _hashTagList };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }
    }
}
