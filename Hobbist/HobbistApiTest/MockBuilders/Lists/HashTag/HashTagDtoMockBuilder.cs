using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HobbistApiTest.MockBuilders.Lists.HashTag
{
    public static class HashTagDtoMockBuilder
    {
        public static HashTagDto GetSingle()
        {
            var dto = new HashTagDto()
            {
                Popularity = new Random().Next()
            };

            dto.HashTagName = dto.GetHashCode().ToString();

            return dto;
        }

        public static List<HashTagDto> GetListByParamLength(int i)
        {
            var dtoList = new List<HashTagDto>();

            for(int j = 0; j < i; j++)
            {
                dtoList.Add(GetSingle());
            }

            return dtoList;
        }
    }
}
