namespace Euphorolog.UnitTests.Controller.ControllerTests
{
    public class DeleteStoryAsyncTests
    {
        /*
         * => 200 Status with Ok Object - done
         * => Expected Value returned from controller matches expected type for multiple test cases - done 
         * => Expected Value returned from controller matches expected value for multiple test cases - done
         * => Service called once with expected parameter and once for multiple test cases - done
         * => Service returns an exception then an exception should be raised - done
         */

        [Theory]
        [InlineData("random")]
        public async void DeleteStoryAsync_OnSuccess_Returns200OkObject(string storyId)
        {
            ///Arrange
            PaginationFilter validFilter = new PaginationFilter(1, 10);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.DeleteStoryAsync(storyId))
                .Returns(true);
            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(1, 10))
                .Returns(StoryControllerFixtures.AllTestStoryResponse());
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Returns(StoryControllerFixtures.AllTestStoriesCount());
            ///Act
            var result = await _storiesController.DeleteStoryAsync(storyId);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.StatusCode.Should().Be(200);
        }
        [Theory]
        [InlineData("random")]
        public async void DeleteStoryAsync_OnSuccess_ReturnsExpectedResponseType(string storyId)
        {
            ///Arrange
            PaginationFilter validFilter = new PaginationFilter(1, 10);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.DeleteStoryAsync(storyId))
                .Returns(true);
            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(1, 10))
                .Returns(StoryControllerFixtures.AllTestStoryResponse());
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Returns(StoryControllerFixtures.AllTestStoriesCount());
            ///Act
            var result = await _storiesController.DeleteStoryAsync(storyId);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.Value.Should().BeOfType<PagedResponse<List<GetStoryResponseDTO>>>();
        }
        [Theory]
        [InlineData("random")]
        public async void DeleteStoryAsync_OnSuccess_ReturnsExpectedResponseValue(string storyId)
        {
            ///Arrange
            PaginationFilter validFilter = new PaginationFilter(1, 10);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.DeleteStoryAsync(storyId))
                .Returns(true);
            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(1, 10))
                .Returns(StoryControllerFixtures.AllTestStoryResponse());
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Returns(StoryControllerFixtures.AllTestStoriesCount());
            ///Act
            var result = await _storiesController.DeleteStoryAsync(storyId);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.Value.Should().BeEquivalentTo(StoryControllerFixtures.PagedAllTestStoryResponse(validFilter.pageNumber, validFilter.pageSize));
        }
        [Theory]
        [InlineData("random")]
        public async void DeleteStoryAsync_Always_ServiceCalledWithExpectedParametersOnce(string storyId)
        {
            ///Arrange
            PaginationFilter validFilter = new PaginationFilter(1, 10);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.DeleteStoryAsync(storyId));
            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(1, 10))
                .Returns(StoryControllerFixtures.AllTestStoryResponse());
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Returns(StoryControllerFixtures.AllTestStoriesCount());
            ///Act
            var result = await _storiesController.DeleteStoryAsync(storyId);
            ///Assert
            A.CallTo(() => FakeStoriesServices.DeleteStoryAsync(storyId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(validFilter.pageNumber, validFilter.pageSize)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync()).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData("random")]
        public async void DeleteStoryAsync_OnExceptionThrownFromServiceDeleteStoryAsync_ExceptionIsRaisedWithGivenMessage(string storyId)
        {
            ///Arrange
            PaginationFilter validFilter = new PaginationFilter(1, 10);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.DeleteStoryAsync(storyId))
                .Throws(new Exception("This is an Exception From DeleteStoryAsync"));
            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(1, 10))
                .Returns(StoryControllerFixtures.AllTestStoryResponse());
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Returns(StoryControllerFixtures.AllTestStoriesCount());
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesController.DeleteStoryAsync(storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an Exception From DeleteStoryAsync");
        }
        [Theory]
        [InlineData("random")]
        public async void DeleteStoryAsync_OnExceptionThrownFromServiceGetAllStoriesAsync_ExceptionIsRaisedWithGivenMessage(string storyId)
        {
            ///Arrange
            PaginationFilter validFilter = new PaginationFilter(1, 10);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.DeleteStoryAsync(storyId))
                .Returns(true);
            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(1, 10))
                .Throws(new Exception("This is an Exception From GetAllStoriesAsync"));
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Returns(StoryControllerFixtures.AllTestStoriesCount());
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesController.DeleteStoryAsync(storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an Exception From GetAllStoriesAsync");
        }
        [Theory]
        [InlineData("random")]
        public async void DeleteStoryAsync_OnExceptionThrownFromServiceGetTotalStoryCountAsync_ExceptionIsRaisedWithGivenMessage(string storyId)
        {
            ///Arrange
            PaginationFilter validFilter = new PaginationFilter(1, 10);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.DeleteStoryAsync(storyId))
                .Returns(true);
            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(1, 10))
                .Returns(StoryControllerFixtures.AllTestStoryResponse());
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Throws(new Exception("This is an Exception From GetTotalStoryCountAsync"));
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesController.DeleteStoryAsync(storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an Exception From GetTotalStoryCountAsync");
        }
    }
}
