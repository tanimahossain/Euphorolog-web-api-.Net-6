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
    public class GetTotalStoryCountAsyncTests
    {
        /*
        *** Repository
        * => Expected Value is correct
        * => context returns an exception then an exception should be raised
        * => Multiple test case check
        */
        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(0)]
        public async void GetTotalStoryCountAsync_OnSuccess_StoryCountIsCorrect(int ind)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.GetTotalStoryCountAsync();
            ///Assert
            response.Should().Be(ind);
        }
        [Fact]
        public async void GetTotalStoryCountAsync_DatabaseExceptionThrown_ExceptionRaised()
        {
            /////Arrange
            //var FakeContext = ContextGenerator.Generate();
            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            //int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            //for (int i = 0; i < ind; i++)
            //{
            //    FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            //}
            //await FakeContext.SaveChangesAsync();
            /////Act
            //var response = await _storiesRepository.GetTotalStoryCountAsync();
            /////Assert
            //response.Should().Be(16);
        }
    }
}
