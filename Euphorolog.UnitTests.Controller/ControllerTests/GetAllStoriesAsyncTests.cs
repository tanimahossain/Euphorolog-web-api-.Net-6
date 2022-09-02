namespace Euphorolog.UnitTests.Controller.ControllerTests
{
    public class GetAllStoriesAsyncTest
    {

        /*
         * => 200 Status with Ok Object - done
         * => Expected Value returned from controller matches expected type for multiple test cases - done 
         * => Expected Value returned from controller matches expected value for multiple test cases - done
         * => Service called once with expected parameter and once for multiple test cases - done
         * => Service returns an exception then an exception should be raised - done
         */

        [Theory]
        [InlineData(1, 10)]
        public async void GetAllStoriesAsync_OnSuccess_Returns200OkObject(int pageNumber, int pageSize)
        {
            ///Arrange
            PaginationFilter filter = new PaginationFilter();
            filter.pageNumber = pageNumber;
            filter.pageSize = pageSize;
            PaginationFilter validFilter = new PaginationFilter(pageNumber, pageSize);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(validFilter.pageNumber, validFilter.pageSize))
                .Returns(StoryControllerFixtures.AllTestStoryResponse());
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Returns(StoryControllerFixtures.AllTestStoriesCount());
            ///Act
            var result = await _storiesController.GetAllStoriesAsync(filter);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.StatusCode.Should().Be(200);
        }
        [Theory]
        [InlineData(1, 10)]
        [InlineData(-1, 100)]
        [InlineData(50, -100)]
        public async void GetAllStoriesAsync_OnSuccess_ReturnsExpectedResponseType(int pageNumber, int pageSize)
        {
            ///Arrange
            PaginationFilter filter = new PaginationFilter();
            filter.pageNumber = pageNumber;
            filter.pageSize = pageSize;
            PaginationFilter validFilter = new PaginationFilter(pageNumber, pageSize);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(validFilter.pageNumber, validFilter.pageSize))
                .Returns(StoryControllerFixtures.AllTestStoryResponse());
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Returns(StoryControllerFixtures.AllTestStoriesCount());
            ///Act
            var result = await _storiesController.GetAllStoriesAsync(filter);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.Value.Should().BeOfType<PagedResponse<List<GetStoryResponseDTO>>>();
        }
        [Theory]
        [InlineData(1, 10)]
        [InlineData(-1, 100)]
        [InlineData(50, -100)]
        public async void GetAllStoriesAsync_OnSuccess_ReturnsExpectedResponseValue(int pageNumber, int pageSize)
        {
            ///Arrange
            PaginationFilter filter = new PaginationFilter();
            filter.pageNumber = pageNumber;
            filter.pageSize = pageSize;
            PaginationFilter validFilter = new PaginationFilter(pageNumber, pageSize);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(validFilter.pageNumber, validFilter.pageSize))
                .Returns(StoryControllerFixtures.AllTestStoryResponse());
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Returns(StoryControllerFixtures.AllTestStoriesCount());
            ///Act
            var result = await _storiesController.GetAllStoriesAsync(filter);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.Value.Should().BeEquivalentTo(StoryControllerFixtures.PagedAllTestStoryResponse(validFilter.pageNumber, validFilter.pageSize));
            response.Value.Should().BeEquivalentTo(StoryControllerFixtures.PagedAllTestStoryResponse(filter.pageNumber, filter.pageSize));
        }
        [Theory]
        [InlineData(1, 10)]
        [InlineData(-1, 100)]
        [InlineData(50, -100)]
        public async void GetAllStoriesAsync_Always_ServiceCalledWithExpectedParametersOnce(int pageNumber, int pageSize)
        {
            ///Arrange
            PaginationFilter filter = new PaginationFilter();
            filter.pageNumber = pageNumber;
            filter.pageSize = pageSize;
            PaginationFilter validFilter = new PaginationFilter(pageNumber, pageSize);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(validFilter.pageNumber, validFilter.pageSize))
                .Returns(StoryControllerFixtures.AllTestStoryResponse());
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Returns(StoryControllerFixtures.AllTestStoriesCount());
            ///Act
            var result = await _storiesController.GetAllStoriesAsync(filter);
            ///Assert
            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(validFilter.pageNumber, validFilter.pageSize)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync()).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1, 10)]
        public async void GetAllStoriesAsyncc_OnExceptionThrownFromServiceGetAllStoriesAsync_ExceptionIsRaisedWithGivenMessage(int pageNumber, int pageSize)
        {
            ///Arrange
            PaginationFilter filter = new PaginationFilter();
            filter.pageNumber = pageNumber;
            filter.pageSize = pageSize;
            PaginationFilter validFilter = new PaginationFilter(pageNumber, pageSize);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(validFilter.pageNumber, validFilter.pageSize))
                .Throws(new Exception("This is an Exception From GetAllStoriesAsync"));
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Returns(StoryControllerFixtures.AllTestStoriesCount());
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesController.GetAllStoriesAsync(filter));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an Exception From GetAllStoriesAsync");
        }
        [Theory]
        [InlineData(1, 10)]
        public async void GetAllStoriesAsyncc_OnExceptionThrownFromServiceGetTotalStoryCountAsync_ExceptionIsRaisedWithGivenMessage(int pageNumber, int pageSize)
        {
            ///Arrange
            PaginationFilter filter = new PaginationFilter();
            filter.pageNumber = pageNumber;
            filter.pageSize = pageSize;
            PaginationFilter validFilter = new PaginationFilter(pageNumber, pageSize);
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetAllStoriesAsync(validFilter.pageNumber, validFilter.pageSize))
                .Returns(StoryControllerFixtures.AllTestStoryResponse());
            A.CallTo(() => FakeStoriesServices.GetTotalStoryCountAsync())
                .Throws(new Exception("This is an Exception From GetTotalStoryCountAsync"));
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesController.GetAllStoriesAsync(filter));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an Exception From GetTotalStoryCountAsync");
        }
    }
}
