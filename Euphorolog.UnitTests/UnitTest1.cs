using Euphorolog.Controllers;
using Euphorolog.Services.DTOs.StoriesDTOs;
using Euphorolog.Services.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Euphorolog.Controller.UnitTests.Fixtures;

namespace Euphorolog.UnitTests
{
    public class UnitTest1
    {
        /*
         * Controller returns 200 OK
        returns okobject with an object
        mock function called with same parameter
        called exactly once
        what storyservice returns is returned as response
        if _storyService.GetStoryAsync throws exception, exception should be raised
        */
        [Theory]
        [InlineData("abc")]
        public async void GetStoryByIdAsync_OnSuccess_Returns200OkObject(string storyId)
        {
            ///Arrange
            DateTime x = DateTime.UtcNow;
            var ReturnStory = A.Fake<GetStoryResponseDTO>();
            var FakeStoriesServices = A.Fake<IStoriesService>();
            StoriesController _storiesController = new StoriesController(FakeStoriesServices);

            A.CallTo(() => FakeStoriesServices.GetStoryByIdAsync(storyId)).Returns(ReturnStory);
            ///Act
            var result = await _storiesController.GetStoryByIdAsync(storyId);
            ///Assert
            result.Should().BeOfType<OkObjectResult>();
            var response = (OkObjectResult)result;
            response.StatusCode.Should().Be(200);
        }
        [Theory]
        [InlineData("tanima-1")]
        public async void GetStoryByIdAsync_OnSuccess_ReturnsPagedResponse(string storyId)
        {
            Assert.True(true);
        }
        [Fact]
        public void check()
        {
            Assert.True(true);
        }
    }
}