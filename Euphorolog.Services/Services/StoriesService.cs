using AutoMapper;
using Euphorolog.Database.Models;
using Euphorolog.Repository.Contracts;
using Euphorolog.Services.Contracts;
using Euphorolog.Services.CustomExceptions;
using Euphorolog.Services.DTOs.StoriesDTOs;
using Euphorolog.Services.DTOValidators;
using Microsoft.AspNetCore.Http;

namespace Euphorolog.Services.Services
{
    public class StoriesService : IStoriesService
    {
        private readonly IStoriesRepository _storiesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IUtilities _utils;
        private readonly IMapper _mapper;
        private readonly MainDTOValidator<PostStoryRequestDTO> _postStoryValidator;
        private readonly MainDTOValidator<UpdateStoryRequestDTO> _updateStoryValidator;
        public StoriesService(
            IStoriesRepository storiesRepository,
            IUsersRepository usersRepository,
            IUtilities utils,
            IMapper mapper,
            MainDTOValidator<PostStoryRequestDTO> postStoryValidator,
            MainDTOValidator<UpdateStoryRequestDTO> updateStoryValidator)
        {
            _storiesRepository = storiesRepository;
            _usersRepository = usersRepository;
            _utils = utils;
            _mapper = mapper;
            _postStoryValidator = postStoryValidator;
            _updateStoryValidator = updateStoryValidator;
        }
        public async Task<int> GetTotalStoryCountAsync()
        {
            var ret = await _storiesRepository.GetTotalStoryCountAsync();
            return ret;

        }
        public async Task<List<GetStoryResponseDTO>> GetAllStoriesAsync(int pageNumber, int pageSize)
        {
            var ret = await _storiesRepository.GetAllStoriesAsync(pageNumber,pageSize);
            return _mapper.Map<List<GetStoryResponseDTO>>(ret);

        }
        public async Task<int> GetTotalStoryCountOfAUserAsync(string username)
        {
            var ret = await _storiesRepository.GetTotalStoryCountOfAUserAsync(username);
            return ret;

        }
        public async Task<List<GetStoryResponseDTO>> GetStoriesByUserIdAsync(int pageNumber, int pageSize, string username)
        {
            var ret = await _storiesRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username);
            return _mapper.Map<List<GetStoryResponseDTO>>(ret);

        }
        public async Task<GetStoryResponseDTO> GetStoryByIdAsync(string id)
        {
            var ret = await _storiesRepository.GetStoryByIdAsync(id);
            if(ret == null)
            {
                throw new NotFoundException("Story not Found.");
            }
            GetStoryResponseDTO story = _mapper.Map<GetStoryResponseDTO>(ret);
            return story;

        }
        public async Task<GetStoryResponseDTO> PostStoryAsync(PostStoryRequestDTO req)
        {
            /*
             * If the token is valid based on time
             */

            _postStoryValidator.ValidateDTO(req);
            // user logged in
            var username = _utils.GetJWTTokenUsername();
            // token valid based on time
            var passChangedAt = await _usersRepository.GetPasswordChangedAtAsync(username);
            if (!_utils.tokenStillValid(passChangedAt))
            {
                throw new UnAuthorizedException("LogIn Again!");
            }

            Stories story = _mapper.Map<Stories>(req);
            var mxreturned = await _storiesRepository.GetMaxStoryNoByUserIdAsync(username);
            int mx = 0;
            if (mxreturned != null) mx = (int)mxreturned;
            story.storyId = $"{username}-{(mx + 1)}";

            story.authorName = username;
            story.storyNo = mx + 1;

            story.createdAt = _utils.GetDateTimeUTCNow();
            story.updatedAt = _utils.GetDateTimeUTCNow();

            var ret = await _storiesRepository.PostStoryAsync(story);
            return _mapper.Map <GetStoryResponseDTO>(ret);
        }
        public async Task<bool> DeleteStoryAsync(string id)
        {
            /*
             * If the token is valid based on time
             * If the story Exists
             * If the story is their's
             */


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
        public async Task<GetStoryResponseDTO> UpdateStoryAsync(string id, UpdateStoryRequestDTO req)
        {
            /*
             * If the token is valid based on time
             * If the story Exists
             * If the story is their's
             */

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
            if(changedStory.storyTitle != null || changedStory.storyDescription != null)
            {
                changedStory.updatedAt = _utils.GetDateTimeUTCNow();
            }
            var ret = await _storiesRepository.UpdateStoryAsync(id, changedStory);
            return _mapper.Map<GetStoryResponseDTO>(ret);
        }
    }
}
