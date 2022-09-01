using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs.StoriesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.UnitTests.Service.ServicesFixtures
{
    public class StoryServicesFixtures
    {
        /// format: year, month, day, hour, minute, second, datetime kind
        private static DateTime DemoDateTime = new DateTime(2022, 8, 28, 16, 32, 0, DateTimeKind.Utc);
        public static DateTime GetDateTime() => DemoDateTime;
        /// PostStoryRequestDTO
        private static PostStoryRequestDTO PostStoryDTO = new PostStoryRequestDTO
        {
            storyTitle = "title",
            storyDescription = "description"
        };
        public static PostStoryRequestDTO GetTestPostStoryDTO() => PostStoryDTO;
        /// UpdateStoryRequestDTO
        private static UpdateStoryRequestDTO UpdateStoryDTO = new UpdateStoryRequestDTO
        {
            storyTitle = "title",
            storyDescription = "description"
        };
        public static UpdateStoryRequestDTO GetTestUpdateStoryDTO() => UpdateStoryDTO;
        /// GetStoryResponseDTO
        private static List<GetStoryResponseDTO> StoriesDTO = new List<GetStoryResponseDTO>()
        {
            new GetStoryResponseDTO
            {
                storyId = "tanima-1",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new GetStoryResponseDTO
            {
                storyId = "string-2",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new GetStoryResponseDTO
            {
                storyId = "tanima-3",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new GetStoryResponseDTO
            {
                storyId = "tanimahossain-3",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new GetStoryResponseDTO
            {
                storyId = "tanima-2",
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            }
        };
        public static List<GetStoryResponseDTO> GetAllTestStoryResponseDTO() => StoriesDTO;
        public static GetStoryResponseDTO GetTestStoryResponseDTO(int id) => StoriesDTO[id];
        public static int GetAllTestStoriesDTOCount() => StoriesDTO.Count;
        /// Stories
        private static List<Stories> Stories = new List<Stories>()
        {
            new Stories
            {
                storyId = "tanima-1",
                storyNo = 1,
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new Stories
            {
                storyId = "string-2",
                storyNo = 1,
                storyTitle = "title",
                authorName = "string",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new Stories
            {
                storyId = "tanima-3",
                storyNo = 3,
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new Stories
            {
                storyId = "tanimahossain-3",
                storyNo = 3,
                storyTitle = "title",
                authorName = "tanimahossain",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new Stories
            {
                storyId = "tanima-2",
                storyNo = 2,
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            }
        };
        private static List<Stories> UpdatedStories = new List<Stories>()
        {
            new Stories
            {
                storyId = "tanima-1",
                storyNo = 1,
                storyTitle = "titleupdated",
                authorName = "tanima",
                storyDescription = "descriptionUpdated",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new Stories
            {
                storyId = "string-2",
                storyNo = 1,
                storyTitle = "title",
                authorName = "string",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new Stories
            {
                storyId = "tanima-3",
                storyNo = 3,
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new Stories
            {
                storyId = "tanimahossain-3",
                storyNo = 3,
                storyTitle = "title",
                authorName = "tanimahossain",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new Stories
            {
                storyId = "tanima-2",
                storyNo = 2,
                storyTitle = "title",
                authorName = "tanima",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            },
            new Stories
            {
                storyId = "string-3",
                storyNo = 2,
                storyTitle = "title",
                authorName = "string",
                storyDescription = "description",
                createdAt = DemoDateTime,
                updatedAt = DemoDateTime
            }
        };
        public static Stories GetTestStory(int id) => Stories[id];
        public static Stories GetUpdatedTestStory(int id) => UpdatedStories[id];
        public static List<Stories> GetAllTestStory() => Stories;
        public static List<Stories> GetAllUpdatedTestStory() => UpdatedStories;
        public static List<Stories> GetAllPagedTestStories(int pageNumber, int pageSize)
        {
            List<Stories> PagedStories = new List<Stories>();
            int b = (pageNumber - 1) * pageSize;
            int e = Math.Min(b + pageSize, Stories.Count);
            for (int i = b; i < e; i++)
            {
                PagedStories.Add(Stories[i]);
            }
            return PagedStories;
        }
        public static List<Stories> GetAllPagedTestStoriesById(int pageNumber, int pageSize, string id)
        {
            List<Stories> UserStories = new List<Stories>();
            for (int i = 0; i < Stories.Count; i++)
            {
                if (Stories[i].authorName == id)
                {
                    UserStories.Add(Stories[i]);
                }
            }
            List<Stories> PagedStories = new List<Stories>();
            int b = (pageNumber - 1) * pageSize;
            int e = Math.Min(b + pageSize, UserStories.Count);
            for (int i = b; i < e; i++)
            {
                PagedStories.Add(UserStories[i]);
            }
            return PagedStories;
        }
        public static int GetAllTestStoriesCount() => Stories.Count;
        public static int GetAllTestStoriesCountById(string id)
        {
            int cnt = 0;
            for (int i = 0; i < Stories.Count; i++)
            {
                if (Stories[i].authorName == id)
                {
                    cnt++;
                }
            }
            return cnt;
        }
        public static int GetAllTestStoriesMaxById(string id)
        {
            int mx = 0;
            for (int i = 0; i < Stories.Count; i++)
            {
                if (Stories[i].authorName == id)
                {
                    mx = Math.Max(mx, Stories[i].storyNo);
                }
            }
            return mx;
        }
    }
}
