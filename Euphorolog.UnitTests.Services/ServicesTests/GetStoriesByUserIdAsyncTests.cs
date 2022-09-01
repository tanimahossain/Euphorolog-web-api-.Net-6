namespace Euphorolog.UnitTests.Service.ServicesTests
{
    public class GetStoriesByUserIdAsyncTests
    {
        public static bool CheckObjectEquality(Stories A, Stories B)
        {
            if (A.storyDescription != B.storyDescription
                || A.storyTitle != B.storyTitle
                || A.authorName != B.authorName
                || A.storyNo != B.storyNo
                || A.storyId != B.storyId
                || A.createdAt != B.createdAt
                || A.updatedAt != B.updatedAt)
                return false;
            return true;
        }
        public static bool CheckObjectEquality(List<Stories> a, List<Stories> b)
        {
            int cnt = a.Count();
            if (cnt != b.Count)
                return false;
            for (int i = 0; i < cnt; i++)
            {
                if (!CheckObjectEquality(a[i], b[i]))
                    return false;
            }
            return true;
        }
        [Theory]
        [InlineData(1, 10, "tanima")]
        public async void GetStoriesByUserIdAsync_OnSuccess_ReturnTypeIsValid(int pageNumber, int pageSize, string username)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStories = StoryServicesFixtures.GetAllTestStory();
            var FakeStoriesDTO = StoryServicesFixtures.GetAllTestStoryResponseDTO();
            A.CallTo(() => FakeMapper
            .Map<List<GetStoryResponseDTO>>(A<List<Stories>>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStories))))
                .Returns(FakeStoriesDTO);
            A.CallTo(() => FakeStoryRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username)).Returns(FakeStories);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetStoriesByUserIdAsync(pageNumber, pageSize, username);
            ///Assert
            response.Should().BeOfType<List<GetStoryResponseDTO>>();
        }
        [Theory]
        [InlineData(1, 10, "string")]
        public async void GetStoriesByUserIdAsync_OnSuccess_ReturnValueIsValid(int pageNumber, int pageSize, string username)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStories = StoryServicesFixtures.GetAllTestStory();
            var FakeStoriesDTO = StoryServicesFixtures.GetAllTestStoryResponseDTO();
            A.CallTo(() => FakeMapper
            .Map<List<GetStoryResponseDTO>>(A<List<Stories>>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStories))))
                .Returns(FakeStoriesDTO);
            A.CallTo(() => FakeStoryRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username)).Returns(FakeStories);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetStoriesByUserIdAsync(pageNumber, pageSize, username);
            ///Assert
            response.Should().BeEquivalentTo(FakeStoriesDTO);
        }
        [Theory]
        [InlineData(1, 10, "tanimahossain")]
        public async void GetStoriesByUserIdAsync_WhenCalled_MapperCalledWithExpectedParameter(int pageNumber, int pageSize, string username)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStories = StoryServicesFixtures.GetAllTestStory();
            var FakeStoriesDTO = StoryServicesFixtures.GetAllTestStoryResponseDTO();
            A.CallTo(() => FakeMapper
            .Map<List<GetStoryResponseDTO>>(A<List<Stories>>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStories))))
                .Returns(FakeStoriesDTO);
            A.CallTo(() => FakeStoryRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username)).Returns(FakeStories);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetStoriesByUserIdAsync(pageNumber, pageSize, username);
            ///Assert
            A.CallTo(() => FakeMapper
            .Map<List<GetStoryResponseDTO>>(A<List<Stories>>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStories)))).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1, 10, "Username")]
        public async void GetStoriesByUserIdAsync_WhenCalled_GetAllStoriesAsyncCalledWithExpectedParameter(int pageNumber, int pageSize, string username)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStories = StoryServicesFixtures.GetAllTestStory();
            var FakeStoriesDTO = StoryServicesFixtures.GetAllTestStoryResponseDTO();
            A.CallTo(() => FakeMapper
            .Map<List<GetStoryResponseDTO>>(A<List<Stories>>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStories))))
                .Returns(FakeStoriesDTO);
            A.CallTo(() => FakeStoryRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username)).Returns(FakeStories);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetStoriesByUserIdAsync(pageNumber, pageSize, username);
            ///Assert
            A.CallTo(() => FakeStoryRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1, 10, "error")]
        public async void GetStoriesByUserIdAsync_StoryNotFound_EmptyArrayReturned(int pageNumber, int pageSize, string username)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStories = StoryServicesFixtures.GetAllPagedTestStoriesById(1, 10, username);
            var FakeStoriesDTO = StoryServicesFixtures.GetAllPagedTestStoriesByIdDTO(1, 10, username);
            A.CallTo(() => FakeMapper
            .Map<List<GetStoryResponseDTO>>(A<List<Stories>>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStories))))
                .Returns(FakeStoriesDTO);
            A.CallTo(() => FakeStoryRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username)).Returns(FakeStories);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetStoriesByUserIdAsync(pageNumber, pageSize, username);
            ///Assert
            response.Should().BeEquivalentTo(FakeStoriesDTO);
            response.Count.Should().Be(0);
        }
        [Theory]
        [InlineData(1, 10, "tanima")]
        public async void GetStoriesByUserIdAsync_MapperExceptionThrown_ExceptionRaised(int pageNumber, int pageSize, string username)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStories = StoryServicesFixtures.GetAllTestStory();
            var FakeStoriesDTO = StoryServicesFixtures.GetAllTestStoryResponseDTO();
            A.CallTo(() => FakeMapper
            .Map<List<GetStoryResponseDTO>>(A<List<Stories>>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStories))))
                .Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeStoryRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username)).Returns(FakeStories);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.GetStoriesByUserIdAsync(pageNumber, pageSize, username));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1, 10, "string")]
        public async void GetStoriesByUserIdAsync_DatabaseExceptionThrown_ExceptionRaised(int pageNumber, int pageSize, string username)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStories = StoryServicesFixtures.GetAllTestStory();
            var FakeStoriesDTO = StoryServicesFixtures.GetAllTestStoryResponseDTO();
            A.CallTo(() => FakeMapper
            .Map<List<GetStoryResponseDTO>>(A<List<Stories>>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStories))))
                .Returns(FakeStoriesDTO);
            A.CallTo(() => FakeStoryRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username)).Throws(new Exception("This is an demo Exception"));
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.GetStoriesByUserIdAsync(pageNumber, pageSize, username));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
    }
}
