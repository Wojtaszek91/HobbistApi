using FluentAssertions;
using HobbistApi.CQRS.Commands.UserProfile.RemoveHashtagCommand;
using HobbistApiTest.MockBuilders;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Profile
{
    public class RemoveHashTagFromProfileCommandTest
    {
        private readonly CancellationToken _cancellationToken;
        private readonly Mock<ILogger<RemoveHashtagFromProfileCommandHandler>> _mockLogger;
        private readonly string _hashTagName;
        private readonly Guid _profileId;
        private readonly List<string> _hashTagNameList;

        public RemoveHashTagFromProfileCommandTest()
        {
            _cancellationToken = new CancellationToken();
            _mockLogger = new Mock<ILogger<RemoveHashtagFromProfileCommandHandler>>();

            _hashTagName = "TestHashTagName";

            _profileId = Guid.NewGuid();

            _hashTagNameList = new List<string>()
            {
                "TestHashTagName",
                "AnotherTest",
                "OneMoreTest"
            };
        }

        [Fact]
        public async Task RemoveHashtagFromProfileCommandRequest_WithCorrectParams_ShouldReturnIntOne()
        {
            var handler = new RemoveHashtagFromProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_RemoveHashTagByNameFromUserProfile_ForParamsReturnParamValue(_hashTagName, _profileId, true).Object,
                HashTagRepoMockBuilder.GetHashTagRepo_GetAllHashTagNamesList_ReturnParamList(_hashTagNameList).Object,
                _mockLogger.Object);

            var request = new RemoveHashtagFromProfileCommandRequest() { ProfileId = _profileId, HashtagName = _hashTagName };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task RemoveHashtagFromProfileCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new RemoveHashtagFromProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_RemoveHashTagByNameFromUserProfile_ForParamsReturnParamValue(_hashTagName, _profileId, true).Object,
                HashTagRepoMockBuilder.GetHashTagRepo_GetAllHashTagNamesList_ThrowsException().Object,
                _mockLogger.Object);

            var request = new RemoveHashtagFromProfileCommandRequest() { ProfileId = _profileId, HashtagName = _hashTagName };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }

        [Fact]
        public async Task RemoveHashtagFromProfileCommandRequest_WithHashTagNameNotIncludedIntoList_ShouldReturnIntTwo()
        {
            var handler = new RemoveHashtagFromProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_RemoveHashTagByNameFromUserProfile_ForParamsReturnParamValue(_hashTagName, _profileId, true).Object,
                HashTagRepoMockBuilder.GetHashTagRepo_GetAllHashTagNamesList_ReturnParamList(_hashTagNameList).Object,
                _mockLogger.Object);

            var request = new RemoveHashtagFromProfileCommandRequest() { ProfileId = _profileId, HashtagName = "NotHashTagName" };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }

        [Fact]
        public async Task RemoveHashtagFromProfileCommandRequest_WithRepoFalseResponse_ShouldReturnIntZero()
        {
            var handler = new RemoveHashtagFromProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_RemoveHashTagByNameFromUserProfile_ForParamsReturnParamValue(_hashTagName, _profileId, false).Object,
                HashTagRepoMockBuilder.GetHashTagRepo_GetAllHashTagNamesList_ReturnParamList(_hashTagNameList).Object,
                _mockLogger.Object);

            var request = new RemoveHashtagFromProfileCommandRequest() { ProfileId = _profileId, HashtagName = _hashTagName };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }
    }
}
