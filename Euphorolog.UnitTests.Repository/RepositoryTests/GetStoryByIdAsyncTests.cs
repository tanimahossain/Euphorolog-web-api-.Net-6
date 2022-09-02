using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.UnitTests.Repository.RepositoryTests
{
    public class GetStoryByIdAsyncTests
    {
        /*
        *** Repository
        * <Stories?> GetStoryByIdAsync(string id)
        => Expected Value is correct - done
        => Expected type match - done
        => context returns an exception then an exception should be raised
        => Multiple test case check - done
        */
        [Theory]
        [InlineData("tanimahossain-3")]
        [InlineData("tanima-2")]
        public async void GetStoryByIdAsync_OnSuccessNotNull_ReturnTypeIsValid(string storyId)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.GetStoryByIdAsync(storyId);
            ///Assert
            response.Should().BeOfType<Stories?>();
        }
        [Theory]
        [InlineData("tanimahossain-3", 3)]
        [InlineData("tanima-2", 4)]
        public async void GetStoryByIdAsync_OnSuccessNotNull_ReturnValueIsValid(string storyId, int storyInd)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            Stories? toBe = StoryRepositoryFixtures.TestStory(storyInd);
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.GetStoryByIdAsync(storyId);
            ///Assert
            response.Should().BeEquivalentTo(toBe);
        }
        [Theory]
        [InlineData("string-1")]
        public async void GetStoryByIdAsync_OnSuccessNull_ReturnTypeAndValueIsValid(string storyId)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.GetStoryByIdAsync(storyId);
            ///Assert
            response.Should().BeNull();
        }
        [Theory]
        [InlineData("error")]
        public async void GetStoryByIdAsync_DatabaseError_AnExceptionShouldBeRaised(string storyId)
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
            //var exception = await Record.ExceptionAsync(async () => await _storiesRepository.GetStoriesByUserIdAsync(-1, -1, username));
            ////var response = await _storiesRepository.GetStoryByIdAsync(storyId);
            /////Assert
            ////response.Should().BeEquivalentTo(toBe);
            //exception.Should().NotBeNull();
        }
    }
}
