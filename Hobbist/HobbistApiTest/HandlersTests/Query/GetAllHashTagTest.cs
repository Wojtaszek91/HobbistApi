using FluentAssertions;
using HobbistApi.CQRS.Queries.HashTag.GetAllHashTagDtoQuery;
using HobbistApiTest.MockBuilders;
using HobbistApiTest.MockBuilders.Lists.HashTag;
using Microsoft.Extensions.Logging;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HobbistApiTest.HandlersTests.Query
{
    public class GetAllHashTagTest
    {
        private readonly Mock<ILogger<GetAllHashTagDtoQueryHandler>> _loggerMock;
        private readonly CancellationToken _cancellationToken;
        private readonly List<HashTagDto> _hashTagDtoList;
        public GetAllHashTagTest()
        {
            _loggerMock = new Mock<ILogger<GetAllHashTagDtoQueryHandler>>();
            _cancellationToken = new CancellationToken();
            _hashTagDtoList = HashTagDtoMockBuilder.GetListByParamLength(5);
        }

        [Fact]
        public async Task GetAllHashTagDtoQueryRequest_WithCorrectParameters_ShouldReturnListEquivalentToParam()
        {
            var handler = new GetAllHashTagDtoQueryHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_GetallHashTagDto_ReturnParam(_hashTagDtoList).Object,
                _loggerMock.Object);

            var request = new GetAllHashTagDtoQueryRequest()
            {
            };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().BeEquivalentTo(_hashTagDtoList);
        }

        [Fact]
        public async Task GetAllHashTagDtoQueryRequest_WhenThrowException_ShouldReturnNull()
        {
            var handler = new GetAllHashTagDtoQueryHandler(
                HashTagRepoMockBuilder.GetHashTagRepo_GetallHashTagDto_ThrowsException().Object,
                _loggerMock.Object);

            var request = new GetAllHashTagDtoQueryRequest()
            {
            };

            var result = await handler.Handle(request, _cancellationToken);

            result.Should().BeNull();
        }

        //[Theory]
        //[ClassData(typeof(CommandRequests))]
        //public void Test1(IRequest request)
        //{
        //}
        //public class CommandRequests : IEnumerable<object[]>
        //{
        //    private readonly List<object[]> _data = new List<object[]>
        //    {
        //        new object[] {new GetAllHashTagDtoQueryRequest() {
        //            PostId = Guid.NewGuid(),
        //            UserProfileId = Guid.NewGuid(),
        //            Mark = 0}
        //        },

        //    };

        //    public IEnumerator<object[]> GetEnumerator()
        //        => _data.GetEnumerator();

        //    IEnumerator IEnumerable.GetEnumerator()
        //        => GetEnumerator();
        //}
    }
}
