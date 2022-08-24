using AutoMapper;
using Euphorolog.Database.Context;
using Euphorolog.Database.Models;
using Euphorolog.Repository.Contracts;
using Euphorolog.Services.Contracts;
using Euphorolog.Services.CustomExceptions;
using Euphorolog.Services.DTOs.StoriesDTOs;
using Euphorolog.Services.DTOValidators;
using Euphorolog.Services.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.Services
{
    public class StoriesService : IStoriesService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStoriesRepository _storiesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IUtilities _utils;
        private readonly IMapper _mapper;
        private readonly MainDTOValidator<PostStoryRequestDTO> _postStoryValidator;
        private readonly MainDTOValidator<UpdateStoryRequestDTO> _updateStoryValidator;
        public StoriesService(
            IStoriesRepository storiesRepository,
            IUsersRepository usersRepository,
            IHttpContextAccessor httpContextAccessor,
            IUtilities utils,
            IMapper mapper,
            MainDTOValidator<PostStoryRequestDTO> postStoryValidator,
            MainDTOValidator<UpdateStoryRequestDTO> updateStoryValidator)
        {
            _httpContextAccessor = httpContextAccessor;
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
        public async Task<List<GetAllStoriesResponseDTO>> GetAllStoriesAsync(int pageNumber, int pageSize)
        {
            var ret = await _storiesRepository.GetAllStoriesAsync(pageNumber,pageSize);
            return _mapper.Map<List<GetAllStoriesResponseDTO>>(ret);

        }
        public async Task<int> GetTotalStoryCountOfAUserAsync(string username)
        {
            var ret = await _storiesRepository.GetTotalStoryCountOfAUserAsync(username);
            return ret;

        }
        public async Task<List<GetAllStoriesResponseDTO>> GetStoriesByUserIdAsync(int pageNumber, int pageSize, string username)
        {
            var ret = await _storiesRepository.GetStoriesByUserIdAsync(pageNumber, pageSize, username);
            return _mapper.Map<List<GetAllStoriesResponseDTO>>(ret);

        }
        public async Task<GetStoryByIdResponseDTO> GetStoryByIdAsync(string id)
        {
            var ret = await _storiesRepository.GetStoryByIdAsync(id);
            if(ret == null)
            {
                throw new NotFoundException("Story not Found.");
            }
            GetStoryByIdResponseDTO story = _mapper.Map<GetStoryByIdResponseDTO>(ret);
            return story;

        }
        public async Task<GetStoryByIdResponseDTO> PostStoryAsync(PostStoryRequestDTO req)
        {
            /*
             * If the token is valid based on time
             */

            _postStoryValidator.ValidateDTO(req);
            // user logged in
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            // token valid based on time
            if (!_utils.tokenStillValid(await _usersRepository.GetPasswordChangedAtAsync(username)))
            {
                throw new UnAuthorizedException("LogIn Again!");
            }

            Stories story = _mapper.Map<Stories>(req);
            int mx = await _storiesRepository.GetMaxStoryNoByUserId(username);
            story.storyId = $"{username}-{(mx + 1)}";

            story.authorName = username;
            story.storyNo = mx + 1;

            story.createdAt = DateTime.UtcNow;
            story.updatedAt = DateTime.UtcNow;

            var ret = await _storiesRepository.PostStoryAsync(story);
            return _mapper.Map <GetStoryByIdResponseDTO>(ret);
        }
        public async Task DeleteStoryAsync(string id)
        {
            /*
             * If the token is valid based on time
             * If the story Exists
             * If the story is their's
             */


            // token valid based on time
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
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
            await _storiesRepository.DeleteStoryAsync(id);
            return;
        }
        public async Task<GetStoryByIdResponseDTO> UpdateStoryAsync(string id, UpdateStoryRequestDTO req)
        {
            /*
             * If the token is valid based on time
             * If the story Exists
             * If the story is their's
             */

            _updateStoryValidator.ValidateDTO(req);
            // token valid based on time
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
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
            var ret = await _storiesRepository.UpdateStoryAsync(id, changedStory);
            return _mapper.Map<GetStoryByIdResponseDTO>(ret);
        }
    }
}
