namespace Euphorolog.UnitTests.Controller.ControllerTests
{
    public class PostStoryAsyncTests
    {
        /*
         * => 200 Status with Ok Object
         * => Expected Value returned from controller matches expected type for multiple test cases
         * => Expected Value returned from controller matches expected value for multiple test cases
         * => Service called once with expected parameter and once for multiple test cases
         * => Service returns an exception then an exception should be raised
         */
        static PostStoryRequestDTO postStoryInput = StoryControllerFixtures.TestPostStoryDTO();
        [Fact]
        public async void PostStoryByIdAsync_OnSuccess_Returns200OkObject()
        {
            ///Arrange
            int ind = new Random().Next(StoryControllerFixtures.AllTestStoriesCount() - 1);
            var ReturnStory = StoryControllerFixtures.TestStoryResponse(ind);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.PostStoryAsync(postStoryInput)).Returns(ReturnStory);
            ///Act
            var result = await _storiesController.PostStoryAsync(postStoryInput);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.StatusCode.Should().Be(200);
        }
        [Fact]
        public async void PostStoryByIdAsync_OnSuccess_ReturnsExpectedType()
        {
            ///Arrange
            int ind = new Random().Next(StoryControllerFixtures.AllTestStoriesCount() - 1);
            var ReturnStory = StoryControllerFixtures.TestStoryResponse(ind);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.PostStoryAsync(postStoryInput)).Returns(ReturnStory);
            ///Act
            var result = await _storiesController.PostStoryAsync(postStoryInput);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            var responseType = result.GetType();
            response.Value.Should().BeOfType<StoryResponse<GetStoryResponseDTO>>();
        }
        [Fact]
        public async void PostStoryByIdAsync_OnSuccess_ReturnsExpectedValue()
        {
            ///Arrange
            int ind = new Random().Next(StoryControllerFixtures.AllTestStoriesCount() - 1);
            var ReturnStory = StoryControllerFixtures.TestStoryResponse(ind);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.PostStoryAsync(postStoryInput)).Returns(ReturnStory);
            ///Act
            var result = await _storiesController.PostStoryAsync(postStoryInput);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.Value.Should().BeEquivalentTo(StoryControllerFixtures.WrappedTestStoryResponse(ind));
        }
        [Fact]
        public async void PostStoryByIdAsync_Always_ServiceCalledWithExpectedParametersOnce()
        {
            ///Arrange
            int ind = new Random().Next(StoryControllerFixtures.AllTestStoriesCount() - 1);
            var ReturnStory = StoryControllerFixtures.TestStoryResponse(ind);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.PostStoryAsync(postStoryInput)).Returns(ReturnStory);
            ///Act
            var result = await _storiesController.PostStoryAsync(postStoryInput);
            ///Assert
            A.CallTo(() => FakeStoriesServices.PostStoryAsync(postStoryInput)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void PostStoryByIdAsync_OnExceptionThrownFromService_ExceptionIsRaisedWithGivenMessage()
        {
            ///Arrange
            int ind = new Random().Next(StoryControllerFixtures.AllTestStoriesCount() - 1);
            var ReturnStory = StoryControllerFixtures.TestStoryResponse(ind);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.PostStoryAsync(postStoryInput))
                .Throws(new Exception("This is an Exception"));
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesController.PostStoryAsync(postStoryInput));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an Exception");
        }
    }
}
