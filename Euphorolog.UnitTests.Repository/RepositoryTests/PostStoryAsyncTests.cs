using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.UnitTests.Repository.RepositoryTests
{
    public class PostStoryAsyncTests
    {
        /*
        *** Repository
        * <Stories> PostStoryAsync()
        => Expected Value is correct - done
        => Expected type match - done
        => context returns an exception then an exception should be raised
        => Multiple test case check - done
        */
        [Theory]
        [InlineData(5)]
        [InlineData(2)]
        public async void PostStoryAsync_OnSuccess_ReturnTypeIsValid(int SkipInd)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            Stories PostableStory = StoryRepositoryFixtures.TestStory(SkipInd);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            for (int i = 0; i < ind; i++)
            {
                if (i == SkipInd)
                    continue;
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.PostStoryAsync(PostableStory);
            ///Assert
            response.Should().BeOfType<Stories>();
        }
        [Theory]
        [InlineData(5)]
        [InlineData(2)]
        public async void PostStoryAsync_OnSuccess_ReturnValueIsValid(int SkipInd)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            Stories PostableStory = StoryRepositoryFixtures.TestStory(SkipInd);
            Stories toBe = StoryRepositoryFixtures.TestStory(SkipInd);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            for (int i = 0; i < ind; i++)
            {
                if (i == SkipInd)
                    continue;
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.PostStoryAsync(PostableStory);
            ///Assert
            response.Should().BeEquivalentTo(toBe);
        }
        [Fact]
        public async void PostStoryAsync_SendFullNullValue_ReturnException()
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            Stories PostableStory = new Stories();
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesRepository.PostStoryAsync(PostableStory));
            ///Assert
            exception.Should().NotBeNull();
        }
    }
}
