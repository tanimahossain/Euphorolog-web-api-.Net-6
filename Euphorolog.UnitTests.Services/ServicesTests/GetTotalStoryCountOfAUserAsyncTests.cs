using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.UnitTests.Service.ServicesTests
{
    public class GetTotalStoryCountOfAUserAsyncTests
    {
        /*
        *** Repository
        * => Expected Value is correct
        * => context returns an exception then an exception should be raised
        * => Multiple test case check
        */
        [Theory]
        [InlineData("tanima")]
        [InlineData("string")]
        public async void GetTotalStoryCountOfAUserAsync_OnSuccess_StoryCountIsCorrect(string username)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            A.CallTo(() => FakeStoryRepository.GetTotalStoryCountOfAUserAsync(username)).Returns(StoryServicesFixtures.GetAllTestStoriesCountById(username));
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetTotalStoryCountOfAUserAsync(username);
            ///Assert
            response.Should().Be(StoryServicesFixtures.GetAllTestStoriesCountById(username));
        }
        [Theory]
        [InlineData("tanima")]
        [InlineData("string")]
        public async void GetTotalStoryCountOfAUserAsync_WhenCalled_CalledWithExpectedParameter(string username)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            A.CallTo(() => FakeStoryRepository.GetTotalStoryCountOfAUserAsync(username)).Returns(StoryServicesFixtures.GetAllTestStoriesCountById(username));
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetTotalStoryCountOfAUserAsync(username);
            ///Assert
            A.CallTo(() => FakeStoryRepository.GetTotalStoryCountOfAUserAsync(username)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void GetTotalStoryCountOfAUserAsync_DatabaseExceptionThrown_ExceptionRaised()
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            A.CallTo(() => FakeStoryRepository.GetTotalStoryCountOfAUserAsync("error")).Throws(new Exception("This is an demo Exception"));
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.GetTotalStoryCountOfAUserAsync("error"));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
    }
}
