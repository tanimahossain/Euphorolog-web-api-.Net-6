namespace Euphorolog.UnitTests.Controller.ControllerTests
{
    public class GetStoryByIdDownloadAsyncTests
    {
        /*
         * => 200 Status with Ok Object - done
         * => Expected Value returned from controller matches expected type for multiple test cases - done 
         * => Expected Value returned from controller matches expected value for multiple test cases - done
         * => Service called once with expected parameter and once for multiple test cases - done
         * => Service returns an exception then an exception should be raised - done
         */

        [Theory]
        [InlineData("abc")]
        public async void GetStoryByIdDownloadAsync_OnSuccess_Returns200OkObject(string storyId)
        {
            ///Arrange
            var ReturnStory = A.Fake<GetStoryResponseDTO>();
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetStoryByIdAsync(storyId)).Returns(ReturnStory);
            ///Act
            var result = await _storiesController.GetStoryByIdDownloadAsync(storyId);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.StatusCode.Should().Be(200);
        }
        [Theory]
        [InlineData("tanima-1", 0)]
        [InlineData("string-2", 1)]
        [InlineData("tanima-3", 2)]
        [InlineData("tanimahossain-3", 3)]
        [InlineData("tanima-2", 4)]
        public async void GetStoryByIdDownloadAsync_OnSuccess_ReturnsExpectedType(string storyId, int ind)
        {
            ///Arrange
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetStoryByIdAsync(storyId)).Returns(StoryControllerFixtures.TestStoryResponse(ind));
            ///Act
            var result = await _storiesController.GetStoryByIdDownloadAsync(storyId);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            var responseType = result.GetType();
            response.Value.Should().BeOfType<GetStoryResponseDTO>();
        }
        [Theory]
        [InlineData("tanima-1", 0)]
        [InlineData("string-2", 1)]
        [InlineData("tanima-3", 2)]
        [InlineData("tanimahossain-3", 3)]
        [InlineData("tanima-2", 4)]
        public async void GetStoryByIdDownloadAsync_OnSuccess_ReturnsExpectedValue(string storyId, int ind)
        {
            ///Arrange
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetStoryByIdAsync(storyId)).Returns(StoryControllerFixtures.TestStoryResponse(ind));
            ///Act
            var result = await _storiesController.GetStoryByIdDownloadAsync(storyId);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.Value.Should().BeEquivalentTo(StoryControllerFixtures.TestStoryResponse(ind));
        }
        [Theory]
        [InlineData("tanima-1", 0)]
        [InlineData("string-2", 1)]
        [InlineData("tanima-3", 2)]
        [InlineData("tanimahossain-3", 3)]
        [InlineData("tanima-2", 4)]
        public async void GetStoryByIdDownloadAsync_Always_ServiceCalledWithExpectedParametersOnce(string storyId, int ind)
        {
            ///Arrange
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetStoryByIdAsync(storyId)).Returns(StoryControllerFixtures.TestStoryResponse(ind));
            ///Act
            var result = await _storiesController.GetStoryByIdDownloadAsync(storyId);
            ///Assert
            A.CallTo(() => FakeStoriesServices.GetStoryByIdAsync(storyId)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData("random")]

        public async void GetStoryByIdDownloadAsync_OnExceptionThrownFromService_ExceptionIsRaisedWithGivenMessage(string storyId)
        {
            ///Arrange
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetStoryByIdAsync(storyId))
                .Throws(new Exception("This is an Exception"));
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesController.GetStoryByIdDownloadAsync(storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an Exception");
        }
    }
}
