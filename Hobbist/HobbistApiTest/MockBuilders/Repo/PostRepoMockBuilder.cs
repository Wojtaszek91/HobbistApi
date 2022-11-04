using DAL.Repositories.IRepositories;
using Models.Models;
using Moq;
using System;

namespace HobbistApiTest.MockBuilders
{
    public static class PostRepoMockBuilder
    {
        public static Mock<IPostRepository> GetPostRepo_GetPostById_Param(Post post)
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.GetPostById(
                    post.Id))
                .Returns(post);

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_GetPostById_OnAnyIdReturnsNull()
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.GetPostById(
                    It.IsAny<Guid>()))
                .Returns((Post)null);

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_AddFollower_ByParamsReturnValue(Guid postId, Guid followerId, bool returnValue)
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.AddFollower(
                    postId,
                    followerId))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_AddFollower_ThrowsException()
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.AddFollower(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_AddPostView_ByParamsReturnValue(Guid postId, bool returnValue)
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.AddPostView(
                    postId))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_AddPostView_ThrowsException()
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.AddPostView(
                    It.IsAny<Guid>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_BlockPost_ByParamsReturnValue(Guid postId, bool returnValue)
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.BlockPost(
                    postId))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_BlockPost_ThrowsException()
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.BlockPost(
                    It.IsAny<Guid>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_DeletePost_ByParamsReturnValue(Guid postId, bool returnValue)
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.DeletePost(
                    postId))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_DeletePost_ThrowsException()
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.DeletePost(
                    It.IsAny<Guid>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_RemoveFollower_ByParamsReturnValue(Guid postId, Guid followerId, bool returnValue)
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.RemoveFollower(
                    postId,
                    followerId))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_RemoveFollower_ThrowsException()
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.RemoveFollower(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_UnblockPost_ByParamsReturnValue(Guid postId, bool returnValue)
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.UnblockPost(
                    postId))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_UnblockPost_ThrowsException()
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.UnblockPost(
                    It.IsAny<Guid>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_AddPost_ByParamsReturnValue(bool doesPostExist, bool returnValue)
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.DoesPostExists(
                    It.IsAny<Guid>()))
                    .Returns(doesPostExist);

            mock.Setup(x => x.AddPost(
                It.IsAny<Post>()))
                .Returns(returnValue);

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_EditPost_ByParamsReturnValue(bool doesPostExist, bool returnValue)
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.DoesPostExists(
                    It.IsAny<Guid>()))
                    .Returns(doesPostExist);

            mock.Setup(x => x.EditPost(
                It.IsAny<Post>()))
                .Returns(returnValue);

            return mock;
        }

        public static Mock<IPostRepository> GetPostRepo_DoesPostExists_ThrowsException()
        {
            var mock = new Mock<IPostRepository>();

            mock.Setup(x => x.DoesPostExists(
                    It.IsAny<Guid>()))
                    .Throws(new Exception());

            return mock;
        }

    }
}
