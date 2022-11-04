using DAL.Repositories.IRepositories;
using Models.Models;
using Moq;
using System;

namespace HobbistApiTest.MockBuilders
{
    public static class PostMarkRepoMockBuilder
    {
        public static Mock<IPostMarkRepository> GetPostMarkRepo_UpsertMark_ForParamsReturnValue(UserProfile userProfile, Post post, int mark, bool returnValue)
        {
            var mock = new Mock<IPostMarkRepository>();

            mock.Setup(x => x.UpsertMark(
                    userProfile,
                    post,
                    mark))
                .Returns(returnValue);

            return mock;
        }

        public static Mock<IPostMarkRepository> GetPostMarkRepo_UpsertMark_ThrowsException()
        {
            var mock = new Mock<IPostMarkRepository>();

            mock.Setup(x => x.UpsertMark(
                    It.IsAny<UserProfile>(),
                    It.IsAny<Post>(),
                    It.IsAny<int>()))
                .Throws(new Exception());

            return mock;
        }
    }
}
