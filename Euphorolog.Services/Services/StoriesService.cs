using AutoMapper;
using Euphorolog.Database.Context;
using Euphorolog.Database.Models;
using Euphorolog.Repository.Contracts;
using Euphorolog.Services.CustomExceptions;
using Euphorolog.Services.DTOs.StoriesDTOs;
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
        public readonly EuphorologContext _context;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IStoriesRepository _storiesRepository;
        public readonly IUsersRepository _usersRepository;
        public readonly IConfiguration _configuration;
        public readonly IMapper _mapper;
        public StoriesService(
            EuphorologContext context,
            IStoriesRepository storiesRepository,
            IUsersRepository usersRepository,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _storiesRepository = storiesRepository;
            _usersRepository = usersRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<int> TotalStoryNoAsync()
        {
            var ret = await _storiesRepository.TotalStoryNoAsync();
            return ret;

        }
        public async Task<List<GetAllStoriesResponseDTO>> GetAllStoriesAsync(int pageNumber, int pageSize)
        {
            var ret = await _storiesRepository.GetAllStoriesAsync(pageNumber,pageSize);
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
             * If the user is logged in
             * If the token is valid based on time
             */
            // user logged in
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if(username == null)
            {
                throw new UnAuthorizedException("LogIn first!");
            }
            // token valid based on time
            var _utils = new Utilities(_configuration, _httpContextAccessor);
            if (!_utils.tokenStillValid(await _usersRepository.GetPasswordChangedAtAsync(username)))
            {
                throw new UnAuthorizedException("LogIn Again!");
            }

            Stories story = _mapper.Map<Stories>(req);
            int mx = await _storiesRepository.MaxStoryNoByUserId(username);
            story.storyId = $"{username}-{(mx + 1)}";

            story.authorName = username;
            story.storyNo = mx + 1;

            int openingLineLength = Math.Min(100, story.storyDescription.Length);
            story.openingLines = story.storyDescription.Substring(0,openingLineLength) + "...";

            story.createdAt = DateTime.UtcNow;
            story.updatedAt = DateTime.UtcNow;

            var ret = await _storiesRepository.PostStoryAsync(story);
            return _mapper.Map <GetStoryByIdResponseDTO>(ret);
        }
        public async Task<List<GetAllStoriesResponseDTO>> DeleteStoryAsync(string id)
        {
            /*
             * If the user is logged in
             * If the token is valid based on time
             * If the story Exists
             * If the story is their's
             */

            // user logged in
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null)
            {
                throw new UnAuthorizedException("LogIn first!");
            }

            // token valid based on time
            var _utils = new Utilities(_configuration, _httpContextAccessor);
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
            var ret = await _storiesRepository.DeleteStoryAsync(id);
            return _mapper.Map<List<GetAllStoriesResponseDTO>>(ret);
        }
        public async Task<GetStoryByIdResponseDTO> UpdateStoryAsync(string id, UpdateStoryRequestDTO req)
        {
            /*
             * If the user is logged in
             * If the token is valid based on time
             * If the story Exists
             * If the story is their's
             */

            // user logged in
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null)
            {
                throw new UnAuthorizedException("LogIn first!");
            }

            // token valid based on time
            var _utils = new Utilities(_configuration, _httpContextAccessor);
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
            if (changedStory.storyDescription != null)
            {
                int openingLineLength = Math.Min(99, changedStory.storyDescription.Length);
                changedStory.openingLines = changedStory.storyDescription.Substring(0, openingLineLength) + "...";
            }
            var ret = await _storiesRepository.UpdateStoryAsync(id, changedStory);
            return _mapper.Map<GetStoryByIdResponseDTO>(ret);
        }
    }
}
