using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.UnitTests.Repository.RepositoryTests
{
    public class GetStoriesByUserIdAsyncTests
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
        [InlineData(1, 10, "tanimahossain")]
        public async void GetStoriesByUserIdAsync_OnSuccess_ReturnTypeIsValid(int pageNumber, int pageSize, string username)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            List<Stories> toBe = StoryRepositoryFixtures.PagedTestStoriesById(pageNumber, pageSize, username);
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username);
            ///Assert
            response.Should().BeOfType<List<Stories>>();
        }
        [Theory]
        [InlineData(1, 10, "tanima")]
        [InlineData(2, 1 , "tanima")]
        [InlineData(3, 10, "string")]
        public async void GetStoriesByUserIdAsync_OnSuccess_ReturnValueIsValid(int pageNumber, int pageSize, string username)
        {
            ///Arrange
            var FakeContext = ContextGenerator.Generate();
            StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            int ind = StoryRepositoryFixtures.AllTestStoriesCount();
            List<Stories> toBe = StoryRepositoryFixtures.PagedTestStoriesById(pageNumber, pageSize, username);
            for (int i = 0; i < ind; i++)
            {
                FakeContext.stories.Add(StoryRepositoryFixtures.TestStory(i));
            }
            await FakeContext.SaveChangesAsync();
            ///Act
            var response = await _storiesRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username);
            ///Assert
            response.Should().BeEquivalentTo(toBe);
        }
        [Theory]
        [InlineData(1, 10, "error")]
        public async void GetStoriesByUserIdAsync_DatabaseError_AnExceptionShouldBeRaised(int pageNumber, int pageSize, string username)
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
            ////var response = await _storiesRepository.GetStoriesByUserIdAsync(-1, -1, username);
            /////Assert
            ////response.Should().BeEquivalentTo(toBe);
            //exception.Should().NotBeNull();
        }
    }
}
