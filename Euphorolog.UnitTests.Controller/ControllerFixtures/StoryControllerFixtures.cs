namespace Euphorolog.UnitTests.Controller.ControllerFixtures
{
    public class StoryControllerFixtures
    {
        /// format: year, month, day, hour, minute, second, datetime kind
        private static DateTime DemoDateTime = new DateTime(2022, 8, 28, 16, 32, 0, DateTimeKind.Utc);
        private static PostStoryRequestDTO PostStory = new PostStoryRequestDTO
        {
            storyTitle = "title",
            storyDescription = "description"
        };
        private static UpdateStoryRequestDTO UpdateStory = new UpdateStoryRequestDTO
        {
            storyTitle = "title",
            storyDescription = "description"
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
                storyId = "string-2",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new GetStoryResponseDTO
            {
                storyId = "tanima-3",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new GetStoryResponseDTO
            {
                storyId = "tanimahossain-3",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new GetStoryResponseDTO
            {
                storyId = "tanima-2",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            }
        };
        public static UpdateStoryRequestDTO TestUpdateStoryDTO() => UpdateStory;
        public static PostStoryRequestDTO TestPostStoryDTO() => PostStory;
        public static List<GetStoryResponseDTO> AllTestStoryResponse() => Stories;
        public static int AllTestStoriesCount() => Stories.Count;
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
