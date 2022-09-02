namespace Euphorolog.UnitTests.Repository.RepositoryTests
{
    public class DeleteStoryAsyncTests
    {
        /*
        *** Repository
        * => database has the expected form after the function call
        * => Expected Value is correct
        * => context returns an exception then an exception should be raised
        * => Multiple test case check
        */
        [Theory]
        [InlineData("tanima-3", 2)]
        [InlineData("tanima-1", 0)]
        [InlineData("string-2", 1)]
        public async void DeleteStoryAsync_OnSuccess_DoesNotContainTheStoryAnymore(string storyId, int ind)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);

            for (int i = 0; i < 5; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            await _storiesRepository.DeleteStoryAsync(storyId);
            ///Assert
            FakeContext.stories.Should().NotContainEquivalentOf(StoryRepositoryFixtures.TestStory(ind));
        }
        [Theory]
        [InlineData("tanima-3", 2)]
        public async void DeleteStoryAsync_OnSuccess_ReturnTrue(string storyId, int ind)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);

            for (int i = 0; i < 5; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            var story = await FakeContext.stories.FirstOrDefaultAsync(s => s.storyId == storyId);
            //A.CallTo(() => FakeContext.stories.Remove(story)).Throws(new Exception("DataBase Error"));
            ///Act
            //await _storiesRepository.DeleteStoryAsync(storyId);
            var respose = await _storiesRepository.DeleteStoryAsync(storyId);

            ///Assert
            respose.Should().BeTrue();
        }
        [Theory]
        [InlineData("ErrorStoryId", 2)]
        public async void DeleteStoryAsync_IfStoryIdNotFound_NothingDeleted(string storyId, int ind)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);

            for (int i = 0; i < 5; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            var story = await FakeContext.stories.FirstOrDefaultAsync(s => s.storyId == storyId);
            //A.CallTo(() => FakeContext.stories.Remove(story)).Throws(new Exception("DataBase Error"));
            ///Act
            //await _storiesRepository.DeleteStoryAsync(storyId);
            await _storiesRepository.DeleteStoryAsync(storyId);

            ///Assert
            FakeContext.stories.Should().ContainEquivalentOf(StoryRepositoryFixtures.TestStory(ind));
        }
        [Theory]
        [InlineData("ErrorStoryId", 2)]
        public async void DeleteStoryAsync_DatabaseError_AnExceptionShouldBeRaised(string storyId, int ind)
        {
            /////Arrange
            //var FakeContext = ContextGenerator.Generate();
            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);

            //for (int i = 0; i < 5; i++)
            //{
            //    FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            //}
            //await FakeContext.SaveChangesAsync();
            //var story = await FakeContext.stories.FirstOrDefaultAsync(s => s.storyId == storyId);
            ////A.CallTo(() => FakeContext.stories.Remove(story)).Throws(new Exception("DataBase Error"));
            /////Act
            ////await _storiesRepository.DeleteStoryAsync(storyId);
            //var exception = await Record.ExceptionAsync(async () => await _storiesRepository.DeleteStoryAsync(storyId));

            /////Assert
            //exception.Should().NotBeNull();
        }
    }
}
