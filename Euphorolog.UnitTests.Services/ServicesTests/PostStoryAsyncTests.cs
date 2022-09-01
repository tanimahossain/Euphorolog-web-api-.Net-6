namespace Euphorolog.UnitTests.Service.ServicesTests
{
    public class PostStoryAsyncTests
    {
        /*
        *** Service
        * <Stories> PostStoryAsync()
        => Expected Value is correct - done
        => Expected type match - done
        => Method called with proper parameters intended number of times - done
        => context returns an exception then an exception should be raised - done
        => Multiple test case check - done
        */
        public static bool CheckObjectEquality(PostStoryRequestDTO A, PostStoryRequestDTO B)
        {
            if (A.storyDescription == B.storyDescription
                && A.storyTitle == B.storyTitle)
                return true;
            return false;
        }
        public static bool CheckObjectEquality(UpdateStoryRequestDTO A, UpdateStoryRequestDTO B)
        {
            if (A.storyDescription == B.storyDescription
                && A.storyTitle == B.storyTitle)
                return true;
            return false;
        }
        public static bool CheckObjectEquality(Stories A, Stories B)
        {
            if (A.storyDescription != B.storyDescription
                || A.storyTitle != B.storyTitle
                || A.authorName != B.authorName
                || A.storyNo != B.storyNo
                || A.storyId != B.storyId
                || A.createdAt != B.createdAt
                || A.updatedAt != B.updatedAt)
                return false;
            return true;
        }
        public static bool CheckObjectEquality(GetStoryResponseDTO A, GetStoryResponseDTO B)
        {
            if (A.storyDescription != B.storyDescription
                || A.storyTitle != B.storyTitle
                || A.authorName != B.authorName
                || A.storyId != B.storyId
                || A.createdAt != B.createdAt
                || A.updatedAt != B.updatedAt)
                return false;
            return true;
        }
        /*
         * public async Task<GetStoryResponseDTO> PostStoryAsync(PostStoryRequestDTO req)
        {
            //If the token is valid based on time
            _postStoryValidator.ValidateDTO(req);
            // user logged in
            var username = _utils.GetJWTTokenUsername();
            // token valid based on time
            if (!_utils.tokenStillValid(await _usersRepository.GetPasswordChangedAtAsync(username)))
            {
                throw new UnAuthorizedException("LogIn Again!");
            }
            Stories story = _mapper.Map<Stories>(req);
            var mxreturned = await _storiesRepository.GetMaxStoryNoByUserIdAsync(username);
            int mx = 0;
            if (mxreturned != null) mx = (int) mxreturned;
            story.storyId = $"{username}-{(mx + 1)}";

            story.authorName = username;
            story.storyNo = mx + 1;

            story.createdAt = _utils.GetDateTimeUTCNow();
            story.updatedAt = _utils.GetDateTimeUTCNow();

            var ret = await _storiesRepository.PostStoryAsync(story);
            return _mapper.Map<GetStoryResponseDTO>(ret);
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
        public async void PostStoryAsync_OnSuccess_ReturnValueTypeIsValid(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils,FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.PostStoryAsync(FakePostStoryDTO);
            ///Assert
            response.Should().BeOfType<GetStoryResponseDTO>();
        }
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async void PostStoryAsync_OnSuccess_ReturnValueIsValid(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.PostStoryAsync(FakePostStoryDTO);
            ///Assert
            response.Should().BeEquivalentTo(FakeGetStoryDTO);
        }

        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_WhenCalled_DTOValidatorCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.PostStoryAsync(FakePostStoryDTO);
            ///Assert
            response.Should().BeEquivalentTo(FakeGetStoryDTO);
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_WhenCalled_GetJWTTokenUsernameCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.PostStoryAsync(FakePostStoryDTO);
            ///Assert
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_WhenCalled_GetPasswordChangedAtAsyncCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.PostStoryAsync(FakePostStoryDTO);
            ///Assert
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_WhenCalled_tokenStillValidCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.PostStoryAsync(FakePostStoryDTO);
            ///Assert
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_WhenCalled_GetMaxStoryNoByUserIdAsyncCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.PostStoryAsync(FakePostStoryDTO);
            ///Assert
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_WhenCalled_GetDateTimeUTCNowCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.PostStoryAsync(FakePostStoryDTO);
            ///Assert
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).MustHaveHappenedTwiceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_WhenCalled_PostStoryAsyncCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.PostStoryAsync(FakePostStoryDTO);
            ///Assert
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
               .That.Matches(story => CheckObjectEquality(FakeStory, story)))).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_WhenCalled_MapperCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            //StoriesRepository _storiesRepository = new StoriesRepository(FakeContext);
            ///Act
            var response = await _storiesService.PostStoryAsync(FakePostStoryDTO);
            ///Assert
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_ExceptionThrownFromDTOValidator_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.PostStoryAsync(FakePostStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        ///
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_ExceptionThrownFromGetJWTTokenUsername_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.PostStoryAsync(FakePostStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_ExceptionThrownFromGetPasswordChangedAtAsync_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName))
                .Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);

            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.PostStoryAsync(FakePostStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_ExceptionThrownFromtokenStillValid_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt))
                .Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);


            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.PostStoryAsync(FakePostStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_ExceptionThrownFromGetMaxStoryNoByUserIdAsync_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName))
                .Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);


            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.PostStoryAsync(FakePostStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_ExceptionThrownFromGetDateTimeUTCNow_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);


            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.PostStoryAsync(FakePostStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_ExceptionThrownFromPostStoryAsync_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);


            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.PostStoryAsync(FakePostStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_ExceptionThrownFromMapper_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Throws(new Exception("This is an demo Exception"));
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);


            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.PostStoryAsync(FakePostStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void PostStoryAsync_tokenStillValidIsFalse_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakePostStoryDTO = StoryServicesFixtures.GetTestPostStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakePostStoryRequestDTOValidator
            .ValidateDTO(A<PostStoryRequestDTO>
            .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt))
                .Returns(false);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<PostStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakePostStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetMaxStoryNoByUserIdAsync(FakeStory.authorName)).Returns(FakeStory.storyNo - 1);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.PostStoryAsync(A<Stories>
                .That.Matches(story => CheckObjectEquality(FakeStory, story))))
                .Returns(FakeStory);
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .Returns(FakeGetStoryDTO);
            StoriesService _storiesService = new StoriesService(
                                                    FakeStoryRepository,
                                                    FakeUserRepository,
                                                    FakeUtils, FakeMapper,
                                                    FakePostStoryRequestDTOValidator,
                                                    FakeUpdateStoryRequestDTOValidator);


            ///Act
            var exception = await Record.ExceptionAsync(async () => await _storiesService.PostStoryAsync(FakePostStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("LogIn Again!");
        }
    }
}
