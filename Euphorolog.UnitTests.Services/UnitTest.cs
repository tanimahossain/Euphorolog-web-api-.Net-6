using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.UnitTests.Service
{
    public class UnitTest
    {
        /*
            *** Services
            => Expected Value is correct
            => Repository called with expected parameter
            => Repository called once
            => Repository returns an exception then an exception should be raised
            => Multiple test case check
        */
        /* ///Controller Method that needs to be tested///
         Task<GetStoryResponseDTO> PostStoryAsync(PostStoryRequestDTO story);
         Task<GetStoryResponseDTO> UpdateStoryAsync(string id, UpdateStoryRequestDTO story);
         Task<int> GetTotalStoryCountAsync();
         Task<List<GetStoryResponseDTO>> GetAllStoriesAsync(int pageNumber, int pageSize);
         Task<GetStoryResponseDTO> GetStoryByIdAsync(string id);
         Task<List<GetStoryResponseDTO>> GetStoriesByUserIdAsync(int pageNumber, int pageSize, string username);
         Task<int> GetTotalStoryCountOfAUserAsync(string username);
         Task<bool> DeleteStoryAsync(string id);
         */
    }
}
