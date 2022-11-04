using FluentAssertions;
using HobbistApi.CQRS.Commands.UserProfile.AddHashtagCommand;
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
    public class AddHashTagToProfileTest
    {
        private readonly CancellationToken _cancellationToken;
        private readonly Mock<ILogger<AddHashtagToProfileCommandHandler>> _mockLogger;
        private readonly string _hashTagName;
        private readonly Guid _profileId;
        private readonly List<string> _hashTagNameList;

        public AddHashTagToProfileTest()
        {
            _cancellationToken = new CancellationToken();
            _mockLogger = new Mock<ILogger<AddHashtagToProfileCommandHandler>>();

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
        public async Task AddHashtagToProfileCommandRequest_WithCorrectParams_ShouldReturnIntOne()
        {
            var handler = new AddHashtagToProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_AddHashTagByNameToUserProfile_ForParamsReturnParamValue(_hashTagName, _profileId, true).Object,
                HashTagRepoMockBuilder.GetHashTagRepo_GetAllHashTagNamesList_ReturnParamList(_hashTagNameList).Object,
                _mockLogger.Object);

            var request = new AddHashtagToProfileCommandRequest() { ProfileId = _profileId, HashtagName = _hashTagName };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(1);
        }

        [Fact]
        public async Task AddHashtagToProfileCommandRequest_WithExceptionThrow_ShouldReturnIntTwo()
        {
            var handler = new AddHashtagToProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_AddHashTagByNameToUserProfile_ForParamsReturnParamValue(_hashTagName, _profileId, true).Object,
                HashTagRepoMockBuilder.GetHashTagRepo_GetAllHashTagNamesList_ThrowsException().Object,
                _mockLogger.Object);

            var request = new AddHashtagToProfileCommandRequest() { ProfileId = _profileId, HashtagName = _hashTagName };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }

        [Fact]
        public async Task AddHashtagToProfileCommandRequest_WithHashTagNameNotIncludedIntoList_ShouldReturnIntTwo()
        {
            var handler = new AddHashtagToProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_AddHashTagByNameToUserProfile_ForParamsReturnParamValue(_hashTagName, _profileId, true).Object,
                HashTagRepoMockBuilder.GetHashTagRepo_GetAllHashTagNamesList_ReturnParamList(_hashTagNameList).Object,
                _mockLogger.Object);

            var request = new AddHashtagToProfileCommandRequest() { ProfileId = _profileId, HashtagName = "NotHashTagName" };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(2);
        }

        [Fact]
        public async Task AddHashtagToProfileCommandRequest_WithRepoFalseResponse_ShouldReturnIntZero()
        {
            var handler = new AddHashtagToProfileCommandHandler(
                UserProfileRepoMockBuilder.GetUserProfileRepo_AddHashTagByNameToUserProfile_ForParamsReturnParamValue(_hashTagName, _profileId, false).Object,
                HashTagRepoMockBuilder.GetHashTagRepo_GetAllHashTagNamesList_ReturnParamList(_hashTagNameList).Object,
                _mockLogger.Object);

            var request = new AddHashtagToProfileCommandRequest() { ProfileId = _profileId, HashtagName = _hashTagName };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().Be(0);
        }
    }
}
