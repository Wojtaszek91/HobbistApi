using DAL.Repositories.IRepositories;
using Models.Models;
using Models.Models.DTOs.Profile;
using Moq;
using System;
using System.Collections.Generic;

namespace HobbistApiTest.MockBuilders
{
    public static class UserProfileRepoMockBuilder
    {
        public static Mock<IUserProfileRepository> GetUserProfileRepo_GetProfileById_ReturnsParam(UserProfile userProfile)
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.GetProfileById(
                    userProfile.Id))
                .Returns(userProfile);

            return mock;
        }

        public static Mock<IUserProfileRepository> GetUserProfileRepo_GetProfileById_OnAnyIdReturnsNull()
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.GetProfileById(
                    It.IsAny<Guid>()))
                .Returns((UserProfile)null);

            return mock;
        }

        public static Mock<IUserProfileRepository> GetUserProfileRepo_AddHashTagByNameToUserProfile_ForParamsReturnParamValue(string hashTagName, Guid userProfileId, bool returnValue)
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.AddHashTagByNameToUserProfile(
                    hashTagName,
                    userProfileId))
                .Returns(returnValue);

            return mock;
        }

        public static Mock<IUserProfileRepository> GetUserProfileRepo_RemoveHashTagByNameFromUserProfile_ForParamsReturnParamValue(string hashTagName, Guid userProfileId, bool returnValue)
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.RemoveHashTagByNameFromUserProfile(
                    hashTagName,
                    userProfileId))
                .Returns(returnValue);

            return mock;
        }

        public static Mock<IUserProfileRepository> GetUserProfileRepo_DoesProfileExistTrue_DeleteProfileReturnParam(Guid profileId, bool returnValue)
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.DoesProfileExist(
                    profileId))
                    .Returns(true);

            mock.Setup(x => x.DeleteProfile(
                    profileId))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IUserProfileRepository> GetUserProfileRepo_DoesProfileExist_ForParamReturnParam(Guid profileId, bool returnValue)
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.DoesProfileExist(
                    profileId))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IUserProfileRepository> GetUserProfileRepo_DoesProfileExist_ThrowsException()
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.DoesProfileExist(
                    It.IsAny<Guid>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IUserProfileRepository> GetUserProfileRepo_DoesProfileExistTrue_UpdateProfileHashtagsByListForParamReturnParam(
            Guid profileId,
            List<string> hashTagList,
            bool returnValue)
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.DoesProfileExist(
                    profileId))
                    .Returns(true);

            mock.Setup(x => x.UpdateProfileHashtagsByList(
                    profileId,
                    hashTagList))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IUserProfileRepository> GetUserProfileRepo_UpdateProfile_OnParamReturnParam(UpsertProfileDto userProfileDto, bool returnValue)
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.UpdateProfile(
                    userProfileDto))
                .Returns(returnValue);

            return mock;
        }

        public static Mock<IUserProfileRepository> GetUserProfileRepo_UpdateProfile_ThrowsException()
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.UpdateProfile(
                    It.IsAny<UpsertProfileDto>()))
                .Throws(new Exception());

            return mock;
        }

        public static Mock<IUserProfileRepository> GetUserProfileRepo_UpdateProfilePhotoBase64_ForParamReturnParam(string photoBase64, Guid profileId, bool returnValue)
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.UpdateProfilePhotoBase64(
                    photoBase64,
                    profileId))
                .Returns(returnValue);

            return mock;
        }

        public static Mock<IUserProfileRepository> GetUserProfileRepo_UpdateProfilePhotoBase64_ThrowsException()
        {
            var mock = new Mock<IUserProfileRepository>();

            mock.Setup(x => x.UpdateProfilePhotoBase64(
                    It.IsAny<string>(),
                    It.IsAny<Guid>()))
                .Throws(new Exception());

            return mock;
        }
    }
}
