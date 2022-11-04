using DAL.Repositories.IRepositories;
using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using Moq;
using System;
using System.Collections.Generic;

namespace HobbistApiTest.MockBuilders
{
    public static class HashTagRepoMockBuilder
    {
        public static Mock<IHashTagRepository> GetHashTagRepo_AddPopularity_ByParamsReturnValue(Guid hashTagId, bool returnValue)
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.AddPopularity(
                    hashTagId))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_AddPopularity_ThrowsException()
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.AddPopularity(
                    It.IsAny<Guid>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_DecreasePopularity_ByParamsReturnValue(Guid hashTagId, bool returnValue)
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.DecreasePopuplarity(
                    hashTagId))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_DecreasePopularity_ThrowsException()
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.DecreasePopuplarity(
                    It.IsAny<Guid>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_AddHashTag_ByParamsReturnValue(string hashTagName, bool returnValue)
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.AddHashTag(
                    hashTagName))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_AddHashTag_ThrowsException()
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.AddHashTag(
                    It.IsAny<string>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_DeleteHashTag_ByParamsReturnValue(Guid hashTagId, bool returnValue)
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.DeleteHashTag(
                    hashTagId))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_DeleteHashTag_ThrowsException()
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.DeleteHashTag(
                    It.IsAny<Guid>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_UpdateHashTag_ByParamsReturnValue(HashTagDto hashTagDto, bool returnValue)
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.EditHashTag(
                    hashTagDto))
                    .Returns(returnValue);

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_UpdateHashTag_ThrowsException()
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.EditHashTag(
                    It.IsAny<HashTagDto>()))
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_GetHashTagByName_WithParamNameReturnsParamDto(string hashTagName, HashTag hashTag)
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.GetHashTagByName(
                    hashTagName))
                    .Returns(hashTag);

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_GetAllHashTagNamesList_ReturnParamList(List<string> hashTagNameList)
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.GetAllHashTagNamesList())
                    .Returns(hashTagNameList);

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_GetAllHashTagNamesList_ThrowsException()
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.GetAllHashTagNamesList())
                    .Throws(new Exception());

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_GetallHashTagDto_ReturnParam(List<HashTagDto> hashTagDtos)
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.GetAllHashtagsDto())
                    .Returns(hashTagDtos);

            return mock;
        }

        public static Mock<IHashTagRepository> GetHashTagRepo_GetallHashTagDto_ThrowsException()
        {
            var mock = new Mock<IHashTagRepository>();

            mock.Setup(x => x.GetAllHashtagsDto())
                    .Throws(new Exception());

            return mock;
        }
    }
}
