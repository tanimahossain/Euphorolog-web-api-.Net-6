using Euphorolog.Database.Models;

namespace Euphorolog.UnitTests.Repository.RepositoryFixtures
{
    public class StoryRepositoryFixtures
    {
        /// format: year, month, day, hour, minute, second, datetime kind
        private static DateTime DemoDateTime = new DateTime(2022, 8, 28, 16, 32, 0, DateTimeKind.Utc);
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
        public static List<Stories> AllTestStory() => Stories;
        public static List<Stories> AllUpdatedTestStory() => UpdatedStories;
        public static List<Stories> PagedTestStories(int pageNumber, int pageSize)
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
        public static List<Stories> PagedTestStoriesById(int pageNumber, int pageSize, string id)
        {
            List<Stories> UserStories = new List<Stories>();
            for(int i = 0; i < Stories.Count; i++)
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
        public static int AllTestStoriesCount() => Stories.Count;
        public static int AllTestStoriesCountById(string id)
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
        public static int AllTestStoriesMaxById(string id)
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
        public static Stories TestStory(int id) => Stories[id];
        public static Stories UpdatedTestStory(int id) => UpdatedStories[id];
    }
}
