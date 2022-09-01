using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.UnitTests.Service.ServicesTests
{
    public class DeleteStoryAsyncTest
    {
        /*
        *** Service
        * <Stories> UpdateStoryAsync()
        => Expected Value is correct - done
        => Expected type match - done
        => Method called with proper parameters intended number of times - done
        => context returns an exception then an exception should be raised - done
        => Multiple test case check - done
        */
        /*
        public async Task<bool> DeleteStoryAsync(string id)
        {
             * If the token is valid based on time
             * If the story Exists
             * If the story is their's

            // token valid based on time
            var username = _utils.GetJWTTokenUsername();
            if (!_utils.tokenStillValid(await _usersRepository.GetPasswordChangedAtAsync(username)))
            {
                throw new UnAuthorizedException("LogIn Again!");
            }

            //story Exists
            var story = await _storiesRepository.GetStoryByIdAsync(id);
            if(story == null)
            {
                throw new NotFoundException("Story not Found!");
            }

            // Their Story
            if (!String.Equals(story.authorName, username))
            {
                throw new ForbiddenException("Not your Story!");
            }
            return await _storiesRepository.DeleteStoryAsync(id);
        }
*
        IStoriesRepository storiesRepository,
            IUsersRepository usersRepository,
            IUtilities utils,
            IMapper mapper,
            MainDTOValidator<PostStoryRequestDTO> postStoryValidator,
            MainDTOValidator<UpdateStoryRequestDTO> updateStoryValidator
        */
        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(1)]
        public async void DeleteStoryAsync_OnSuccess_ReturnValueIsTrue(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.DeleteStoryAsync(FakeStory.storyId);
            ///Assert
            response.Should().BeTrue(); ;
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_WhenCalled_GetJWTTokenUsernameCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.DeleteStoryAsync(FakeStory.storyId);
            ///Assert
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_WhenCalled_GetPasswordChangedAtAsyncCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.DeleteStoryAsync(FakeStory.storyId);
            ///Assert
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_WhenCalled_tokenStillValidCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.DeleteStoryAsync(FakeStory.storyId);
            ///Assert
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_WhenCalled_GetStoryByIdAsyncCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.DeleteStoryAsync(FakeStory.storyId);
            ///Assert
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_WhenCalled_DeleteStoryAsyncCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.DeleteStoryAsync(FakeStory.storyId);
            ///Assert
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_ExceptionThrownFromGetJWTTokenUsername_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.DeleteStoryAsync(FakeStory.storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_ExceptionThrownFromGetPasswordChangedAtAsync_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.DeleteStoryAsync(FakeStory.storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_ExceptionThrownFromtokenStillValid_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.DeleteStoryAsync(FakeStory.storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_ExceptionThrownFromGetStoryByIdAsync_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.DeleteStoryAsync(FakeStory.storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_ExceptionThrownFroDeleteStoryAsync_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Throws(new Exception("This is an demo Exception"));
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.DeleteStoryAsync(FakeStory.storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_tokenStillValidIsFalse_ExceptionThrown(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(false);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.DeleteStoryAsync(FakeStory.storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("LogIn Again!");
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_GetStoryByIdAsyncReturnedNull_ExceptionThrown(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(null as Stories);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.DeleteStoryAsync(FakeStory.storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("Story not Found!");
        }
        [Theory]
        [InlineData(0)]
        public async void DeleteStoryAsync_NotTheStoryOfThatUser_ExceptionThrown(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns("NotTheUser");
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync("NotTheUser")).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.DeleteStoryAsync(FakeStory.storyId)).Returns(true);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.DeleteStoryAsync(FakeStory.storyId));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("Not your Story!");
        }
    }
}
