using Euphorolog.Filter;
using Euphorolog.Helpers;
using Euphorolog.Services.DTOs.StoriesDTOs;
using Euphorolog.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Controller.UnitTests.Fixtures
{
    public class ControllerFixture
    {
        /// format: year, month, day, hour, minute, second, datetime kind
        private static DateTime DemoDateTime = new DateTime(2022, 8, 28, 16, 32, 0, DateTimeKind.Utc);
        private static GetStoryResponseDTO Story = new GetStoryResponseDTO
        {
            storyId = "tanima-1",
            storyTitle = "title",
            authorName = "tanima",
            storyDescription = "description",
            createdAt = DemoDateTime,
            updatedAt = DemoDateTime
        };
        private static List<GetStoryResponseDTO> Stories = new List<GetStoryResponseDTO>()
        {
            new GetStoryResponseDTO
            {
                storyId = "tanima-1",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new GetStoryResponseDTO
            {
                storyId = "tanima-1",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new GetStoryResponseDTO
            {
                storyId = "tanima-1",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            }
        };
        public static List<GetStoryResponseDTO> AllTestStoryResponse() => Stories;
        public static GetStoryResponseDTO TestStoryResponse(int id) => Stories[id];
        public static PagedResponse<List<GetStoryResponseDTO>> PagedAllTestStoryResponse(int pageNumber, int pageSize)
        {
            var validFilter = new PaginationFilter(pageNumber, pageSize);
            var totalRecords = Stories.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetStoryResponseDTO>(Stories, validFilter, totalRecords);
            return pagedReponse;
        }
        public static StoryResponse<GetStoryResponseDTO> WrappedTestStoryResponse(int id)
        {
            var wrapped = new StoryResponse<GetStoryResponseDTO>(Stories[id]);
            return wrapped;
        }
    }
}
