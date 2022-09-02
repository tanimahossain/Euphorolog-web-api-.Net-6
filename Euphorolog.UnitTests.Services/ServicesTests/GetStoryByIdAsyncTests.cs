namespace Euphorolog.UnitTests.Service.ServicesTests
{
    public class GetStoryByIdAsyncTests
    {
        /*
        *** Repository
        * => Expected Value is correct
        * => context returns an exception then an exception should be raised
        * => Multiple test case check
        */
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
        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public async void GetStoryByIdAsync_OnSuccess_ReturnTypeIsValid(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeStoryDTO);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStoryDTO.storyId)).Returns(FakeStory);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetStoryByIdAsync(FakeStoryDTO.storyId);
            ///Assert
            response.Should().BeOfType<GetStoryResponseDTO>();
        }
        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public async void GetStoryByIdAsync_OnSuccess_ReturnValueIsValid(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeStoryDTO);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStoryDTO.storyId)).Returns(FakeStory);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetStoryByIdAsync(FakeStoryDTO.storyId);
            ///Assert
            response.Should().Be(FakeStoryDTO);
        }
        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public async void GetStoryByIdAsync_WhenCalled_MapperCalledWithExpectedParameter(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeStoryDTO);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStoryDTO.storyId)).Returns(FakeStory);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetStoryByIdAsync(FakeStoryDTO.storyId);
            ///Assert
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory)))).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public async void GetStoryByIdAsync_WhenCalled_GetStoryByIdAsyncCalledWithExpectedParameter(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeStoryDTO);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStoryDTO.storyId)).Returns(FakeStory);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetStoryByIdAsync(FakeStoryDTO.storyId);
            ///Assert
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStoryDTO.storyId)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void GetStoryByIdAsync_StoryNotFound_ExceptionThrown()
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStory = StoryServicesFixtures.GetTestStory(0);
            var FakeStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(0);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeStoryDTO);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStoryDTO.storyId)).Returns(null as Stories);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.GetStoryByIdAsync(FakeStoryDTO.storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("Story not Found!");
        }
        [Fact]
        public async void GetStoryByIdAsync_MapperExceptionThrown_ExceptionRaised()
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStory = StoryServicesFixtures.GetTestStory(0);
            var FakeStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(0);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStoryDTO.storyId)).Returns(FakeStory);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.GetStoryByIdAsync(FakeStoryDTO.storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Fact]
        public async void GetStoryByIdAsync_DatabaseExceptionThrown_ExceptionRaised()
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeStory = StoryServicesFixtures.GetTestStory(0);
            var FakeStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(0);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeStoryDTO);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStoryDTO.storyId)).Throws(new Exception("This is an demo Exception"));
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.GetStoryByIdAsync(FakeStoryDTO.storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
    }
}
