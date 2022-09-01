namespace Euphorolog.UnitTests.Repository.RepositoryTests
{
    public class GetAllStoriesAsyncTests
    {
        /*
        *** Repository
        * <List<Stories>> GetAllStoriesAsync(int pageNumber, int pageSize)
        => Expected Value is correct - done
        => Expected type match - done
        => context returns an exception then an exception should be raised
        => Multiple test case check
        */
        [Theory]
        [InlineData(1, 10)]
        public async void GetAllStoriesAsync_OnSuccess_ReturnTypeIsValid(int pageNumber, int pageSize)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            List<Stories> toBe = StoryRepositoryFixtures.PagedTestStories(pageNumber, pageSize);
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.GetAllStoriesAsync(pageNumber, pageSize);
            ///Assert
            response.Should().BeOfType<List<Stories>>();
        }
        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 2)]
        [InlineData(3, 10)]
        public async void GetAllStoriesAsync_OnSuccess_ReturnValueIsValid(int pageNumber, int pageSize)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            List<Stories> toBe = StoryRepositoryFixtures.PagedTestStories(pageNumber, pageSize);
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.GetAllStoriesAsync(pageNumber, pageSize);
            ///Assert
            response.Should().BeEquivalentTo(toBe);
        }
        [Theory]
        [InlineData(1,10)]
        public async void GetAllStoriesAsync_DatabaseError_AnExceptionShouldBeRaised(int pageNumber, int pageSize)
        {
            /////Arrange
            //var FakeContext = ContextGenerator.Generate();
            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            //int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            //List<Stories> toBe = StoryRepositoryFixtures.PagedTestStories(pageNumber, pageSize);
            //for (int i = 0; i < ind; i++)
            //{
            //    FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            //}
            //await FakeContext.SaveChangesAsync();
            /////Act
            //var exception = await Record.ExceptionAsync(async () => await _storiesRepository.GetAllStoriesAsync(-1, -1));
            ////var response = await _storiesRepository.GetAllStoriesAsync(-1, -1);
            /////Assert
            ////response.Should().BeEquivalentTo(toBe);
            //exception.Should().NotBeNull();
        }
    }
}
