namespace Euphorolog.UnitTests.Controller.ControllerTests
{
    public class UpdateStoryAsyncTests
    {
        /*
         * => 200 Status with Ok Object
         * => Expected Value returned from controller matches expected type for multiple test cases
         * => Expected Value returned from controller matches expected value for multiple test cases
         * => Service called once with expected parameter and once for multiple test cases
         * => Service returns an exception then an exception should be raised
         */
        static UpdateStoryRequestDTO updateStoryInput = StoryControllerFixtures.TestUpdateStoryDTO();
        [Fact]
        public async void UpdateStoryByIdAsync_OnSuccess_Returns200OkObject()
        {
            ///Arrange
            int ind = new Random().Next(StoryControllerFixtures.AllTestStoriesCount() - 1);
            var ReturnStory = StoryControllerFixtures.TestStoryResponse(ind);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.UpdateStoryAsync(ReturnStory.storyId, updateStoryInput)).Returns(ReturnStory);
            ///Act
            var result = await _storiesController.UpdateStoryAsync(ReturnStory.storyId, updateStoryInput);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.StatusCode.Should().Be(200);
        }
        [Fact]
        public async void UpdateStoryByIdAsync_OnSuccess_ReturnsExpectedType()
        {
            ///Arrange
            int ind = new Random().Next(StoryControllerFixtures.AllTestStoriesCount() - 1);
            var ReturnStory = StoryControllerFixtures.TestStoryResponse(ind);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.UpdateStoryAsync(ReturnStory.storyId, updateStoryInput)).Returns(ReturnStory);
            ///Act
            var result = await _storiesController.UpdateStoryAsync(ReturnStory.storyId, updateStoryInput);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            var responseType = result.GetType();
            response.Value.Should().BeOfType<StoryResponse<GetStoryResponseDTO>>();
        }
        [Fact]
        public async void UpdateStoryByIdAsync_OnSuccess_ReturnsExpectedValue()
        {
            ///Arrange
            int ind = new Random().Next(StoryControllerFixtures.AllTestStoriesCount() - 1);
            var ReturnStory = StoryControllerFixtures.TestStoryResponse(ind);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.UpdateStoryAsync(ReturnStory.storyId, updateStoryInput)).Returns(ReturnStory);
            ///Act
            var result = await _storiesController.UpdateStoryAsync(ReturnStory.storyId, updateStoryInput);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.Value.Should().BeEquivalentTo(StoryControllerFixtures.WrappedTestStoryResponse(ind));
        }
        [Fact]
        public async void UpdateStoryByIdAsync_Always_ServiceCalledWithExpectedParametersOnce()
        {
            ///Arrange
            int ind = new Random().Next(StoryControllerFixtures.AllTestStoriesCount() - 1);
            var ReturnStory = StoryControllerFixtures.TestStoryResponse(ind);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.UpdateStoryAsync(ReturnStory.storyId, updateStoryInput)).Returns(ReturnStory);
            ///Act
            var result = await _storiesController.UpdateStoryAsync(ReturnStory.storyId, updateStoryInput);
            ///Assert
            A.CallTo(() => FakeStoriesServices.UpdateStoryAsync(ReturnStory.storyId, updateStoryInput)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void UpdateStoryByIdAsync_OnExceptionThrownFromService_ExceptionIsRaisedWithGivenMessage()
        {
            ///Arrange
            int ind = new Random().Next(StoryControllerFixtures.AllTestStoriesCount() - 1);
            var ReturnStory = StoryControllerFixtures.TestStoryResponse(ind);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.UpdateStoryAsync(ReturnStory.storyId, updateStoryInput))
                .Throws(new Exception("This is an Exception"));
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesController.UpdateStoryAsync(ReturnStory.storyId, updateStoryInput));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an Exception");
        }
    }
}
