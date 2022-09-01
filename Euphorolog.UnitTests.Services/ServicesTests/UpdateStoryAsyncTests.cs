namespace Euphorolog.UnitTests.Service.ServicesTests
{
    public class UpdateStoryAsyncTests
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
        public async Task<GetStoryResponseDTO> UpdateStoryAsync(string id, UpdateStoryRequestDTO req)
        {
             * If the token is valid based on time
             * If the story Exists
             * If the story is their's

            _updateStoryValidator.ValidateDTO(req);
            // token valid based on time
            var username = _utils.GetJWTTokenUsername();
            if (!_utils.tokenStillValid(await _usersRepository.GetPasswordChangedAtAsync(username)))
            {
                throw new UnAuthorizedException("LogIn Again!");
            }

            //story Exists
            var story = await _storiesRepository.GetStoryByIdAsync(id);
            if (story == null)
            {
                throw new NotFoundException("Story not found");
            }

            // Their Story
            if (!String.Equals(story.authorName, username))
            {
                throw new ForbiddenException("Not your Story!");
            }


            var changedStory = _mapper.Map<Stories>(req);
            if (changedStory.storyTitle != null || changedStory.storyDescription != null)
            {
                changedStory.updatedAt = _utils.GetDateTimeUTCNow();
            }
            var ret = await _storiesRepository.UpdateStoryAsync(id, changedStory);
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
        public async void UpdateStoryAsync_OnSuccess_ReturnValueTypeIsValid(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var response = await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO);
            ///Assert
            response.Should().BeOfType<GetStoryResponseDTO>();
        }
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async void UpdateStoryAsync_OnSuccess_ReturnValueIsValid(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var response = await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO);
            ///Assert
            response.Should().BeEquivalentTo(FakeGetStoryDTO);
        }

        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_WhenCalled_DTOValidatorCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var response = await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO);
            ///Assert
            response.Should().BeEquivalentTo(FakeGetStoryDTO);
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_WhenCalled_GetJWTTokenUsernameCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var response = await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO);
            ///Assert
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_WhenCalled_GetPasswordChangedAtAsyncCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var response = await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO);
            ///Assert
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_WhenCalled_tokenStillValidCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var response = await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO);
            ///Assert
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_WhenCalled_GetStoryByIdAsyncCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var response = await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO);
            ///Assert
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_WhenCalled_GetDateTimeUTCNowCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var response = await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO);
            ///Assert
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_WhenCalled_UpdateStoryAsyncCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var response = await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO);
            ///Assert
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
               .That.Matches(story => CheckObjectEquality(FakeStory, story)))).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_WhenCalled_MapperCalledWithProperParameters(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var response = await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO);
            ///Assert
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(postStoryRequestDTO => CheckObjectEquality(postStoryRequestDTO, FakeUpdateStoryDTO))))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeMapper
            .Map<GetStoryResponseDTO>(A<Stories>
                .That.Matches(stories => CheckObjectEquality(stories, FakeStory))))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_ExceptionThrownFromDTOValidator_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var exception = await Record.ExceptionAsync(async () => await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        ///
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_ExceptionThrownFromGetJWTTokenUsername_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var exception = await Record.ExceptionAsync(async () => await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_ExceptionThrownFromGetPasswordChangedAtAsync_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var exception = await Record.ExceptionAsync(async () => await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_ExceptionThrownFromtokenStillValid_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var exception = await Record.ExceptionAsync(async () => await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_ExceptionThrownFromGetStoryByIdAsync_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var exception = await Record.ExceptionAsync(async () => await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_ExceptionThrownFromGetDateTimeUTCNow_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Throws(new Exception("This is an demo Exception"));
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var exception = await Record.ExceptionAsync(async () => await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_ExceptionThrownFroUpdateStoryAsync_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var exception = await Record.ExceptionAsync(async () => await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_ExceptionThrownFromMapper_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var exception = await Record.ExceptionAsync(async () => await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("This is an demo Exception");
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_tokenStillValidIsFalse_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(false);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var exception = await Record.ExceptionAsync(async () => await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("LogIn Again!");
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_GetStoryByIdAsyncReturnedNull_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns(FakeStory.authorName);
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync(FakeStory.authorName)).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(null as Stories);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var exception = await Record.ExceptionAsync(async () => await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("Story not found");
        }
        [Theory]
        [InlineData(1)]
        public async void UpdateStoryAsync_NotTheStoryOfThatUser_ExceptionRaised(int ind)
        {
            ///Arrange
            var FakeStoryRepository = A.Fake<IStoriesRepository>();
            var FakeUserRepository = A.Fake<IUsersRepository>();
            var FakeMapper = A.Fake<IMapper>();
            var FakePostStoryRequestDTOValidator = A.Fake<MainDTOValidator<PostStoryRequestDTO>>();
            var FakeUpdateStoryRequestDTOValidator = A.Fake<MainDTOValidator<UpdateStoryRequestDTO>>();
            var FakeUtils = A.Fake<IUtilities>();
            string JWTToken = "ThisIsAJWTToken";
            var FakeUpdateStoryDTO = StoryServicesFixtures.GetTestUpdateStoryDTO();
            var FakeStory = StoryServicesFixtures.GetTestStory(ind);
            var FakeGetStoryDTO = StoryServicesFixtures.GetTestStoryResponseDTO(ind);
            DateTime FakePassChangedAt = StoryServicesFixtures.GetDateTime();
            A.CallTo(() => FakeUpdateStoryRequestDTOValidator
            .ValidateDTO(A<UpdateStoryRequestDTO>
            .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
            .DoesNothing();
            A.CallTo(() => FakeUtils.GetJWTTokenUsername()).Returns("NotThisUser");
            A.CallTo(() => FakeUserRepository.GetPasswordChangedAtAsync("NotThisUser")).Returns(FakePassChangedAt);
            A.CallTo(() => FakeUtils.tokenStillValid(FakePassChangedAt)).Returns(true);
            A.CallTo(() => FakeMapper
                .Map<Stories>(A<UpdateStoryRequestDTO>
                .That.Matches(updateStoryRequestDTO => CheckObjectEquality(updateStoryRequestDTO, FakeUpdateStoryDTO))))
                .Returns(FakeStory);
            A.CallTo(() => FakeStoryRepository.GetStoryByIdAsync(FakeStory.storyId)).Returns(FakeStory);
            A.CallTo(() => FakeUtils.GetDateTimeUTCNow()).Returns(FakeStory.createdAt);
            A.CallTo(() => FakeStoryRepository.UpdateStoryAsync(FakeStory.storyId, A<Stories>
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
            var exception = await Record.ExceptionAsync(async () => await _storiesService.UpdateStoryAsync(FakeStory.storyId, FakeUpdateStoryDTO));
            ///Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("Not your Story!");
        }
    }
}
