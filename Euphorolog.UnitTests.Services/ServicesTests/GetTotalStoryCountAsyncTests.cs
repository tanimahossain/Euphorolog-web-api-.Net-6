using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.UnitTests.Service.ServicesTests
{
    public class GetTotalStoryCountAsyncTests
    {
        /*
        *** Repository
        * => Expected Value is correct
        * => context returns an exception then an exception should be raised
        * => Multiple test case check
        */
        [Fact]
        public async void GetTotalStoryCountAsync_OnSuccess_StoryCountIsCorrect()
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            A.CallTo(() => FakeStoryRepository.GetTotalStoryCountAsync()).Returns(StoryServicesFixtures.GetAllTestStoriesCount());
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetTotalStoryCountAsync();
            ///Assert
            response.Should().Be(StoryServicesFixtures.GetAllTestStoriesCount());
        }
        [Fact]
        public async void GetTotalStoryCountAsync_WhenCalled_CalledWithExpectedParameter()
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            A.CallTo(() => FakeStoryRepository.GetTotalStoryCountAsync()).Returns(StoryServicesFixtures.GetAllTestStoriesCount());
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var response = await _storiesService.GetTotalStoryCountAsync();
            ///Assert
            A.CallTo(() => FakeStoryRepository.GetTotalStoryCountAsync()).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void GetTotalStoryCountAsync_DatabaseExceptionThrown_ExceptionRaised()
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            A.CallTo(() => FakeStoryRepository.GetTotalStoryCountAsync()).Throws(new Exception("This is an demo Exception")); ;
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.GetTotalStoryCountAsync());
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
    }
}
