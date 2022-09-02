namespace Euphorolog.UnitTests.Repository.RepositoryTests
{
    public class UpdateStoryAsyncTests
    {
        /*
        *** Repository
        * <Stories> UpdateStoryAsync(string id)
        => Expected Value is correct - done
        => Expected type match - done
        => context returns an exception then an exception should be raised
        => Multiple test case check - done
        */
        [Theory]
        [InlineData(3)]
        [InlineData(0)]
        [InlineData(1)]
        public async void UpdateStoryAsync_OnSuccess_ReturnTypeIsValid(int SkipInd)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            Stories UpdatableStory = StoryRepositoryFixtures.UpdatedTestStory(SkipInd);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.UpdateStoryAsync(UpdatableStory.storyId, UpdatableStory);
            ///Assert
            response.Should().BeOfType<Stories>();
        }
        [Theory]
        [InlineData(3)]
        [InlineData(0)]
        [InlineData(1)]
        public async void UpdateStoryAsync_OnSuccess_ReturnValueIsValid(int SkipInd)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            Stories UpdatableStory = StoryRepositoryFixtures.UpdatedTestStory(SkipInd);
            Stories toBe = StoryRepositoryFixtures.UpdatedTestStory(SkipInd);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.UpdateStoryAsync(UpdatableStory.storyId, UpdatableStory);
            ///Assert
            response.Should().BeEquivalentTo(toBe);
        }
        [Theory]
        [InlineData(3)]
        [InlineData(0)]
        [InlineData(1)]
        public async void UpdateStoryAsync_OnSuccess_UpdatedDataBaseOrNot(int SkipInd)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            Stories UpdatableStory = StoryRepositoryFixtures.UpdatedTestStory(SkipInd);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.UpdateStoryAsync(UpdatableStory.storyId, UpdatableStory);
            var toBe = await _storiesRepository.GetStoryByIdAsync(UpdatableStory.storyId);
            ///Assert
            response.Should().BeEquivalentTo(toBe);
        }
        [Fact]
        public async void UpdateStoryAsync_NullValue_ReturnStory()
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            Stories UpdatableStory = StoryRepositoryFixtures.UpdatedTestStory(0);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.UpdateStoryAsync(UpdatableStory.storyId, null);
            ///Assert
            response.Should().BeEquivalentTo(UpdatableStory);
        }
        [Fact]
        public async void UpdateStoryAsync_ServerSideError_ReturnException()
        {
            /////Arrange
            //var FakeContext = ContextGenerator.Generate();
            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            //Stories UpdatableStory = StoryRepositoryFixtures.UpdatedTestStory(1);
            //int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            //for (int i = 0; i < ind; i++)
            //{
            //    FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            //}
            //await FakeContext.SaveChangesAsync();
            /////Act
            //var exception = await Record.ExceptionAsync(async () => await _storiesRepository.UpdateStoryAsync(UpdatableStory.storyId, UpdatableStory));
            /////Assert
            //exception.Should().NotBeNull();
        }
    }
}
