using Euphorolog.Repository.Repositories;
using Euphorolog.UnitTests.Repository.RepositoryFixtures;
using Euphorolog.UnitTests.Repository.TestDbContext;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.UnitTests.Repository.RepositoryTests
{
    public class GetMaxStoryNoByUserIdAsyncTests
    {
        /*
        *** Repository
        * => Expected Value is correct
        * => context returns an exception then an exception should be raised
        * => Multiple test case check
        */
        [Theory]
        [InlineData("tanima")]
        [InlineData("tanimahossain")]
        [InlineData("string")]
        [InlineData("nonExistent")]
        public async void GetMaxStoryNoByUserIdAsync_OnSuccess_StoryCountIsCorrect(string username)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            int toBe = StoryRepositoryFixtures.AllTestStoriesMaxById(username);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.GetMaxStoryNoByUserIdAsync(username);
            ///Assert
            response.Should().Be(toBe);
        }
        [Theory]
        [InlineData("tanima")]
        public async void GetMaxStoryNoByUserIdAsync_DatabaseExceptionThrown_ExceptionRaised(string storyId)
        {
            /////Arrange
            //var FakeContext = ContextGenerator.Generate();
            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            //int ind = StoryRepositoryFixtures.AllTestStoriesCountById(storyId);
            //for (int i = 0; i < ind; i++)
            //{
            //    FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            //}
            //await FakeContext.SaveChangesAsync();
            /////Act
            //var response = await _storiesRepository.GetMaxStoryNoByUserIdAsync(storyId);
            /////Assert
            //response.Should().Be(16);
        }
    }
}
