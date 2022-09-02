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
        /* ///Services Method that needs to be tested///
         Task<GetStoryResponseDTO> PostStoryAsync(PostStoryRequestDTO story); - done
         Task<GetStoryResponseDTO> UpdateStoryAsync(string id, UpdateStoryRequestDTO story); - done 
         Task<bool> DeleteStoryAsync(string id); - done
         Task<int> GetTotalStoryCountAsync(); - done
         Task<int> GetTotalStoryCountOfAUserAsync(string username); - done
         Task<List<GetStoryResponseDTO>> GetAllStoriesAsync(int pageNumber, int pageSize);
         Task<GetStoryResponseDTO> GetStoryByIdAsync(string id); - done
         Task<List<GetStoryResponseDTO>> GetStoriesByUserIdAsync(int pageNumber, int pageSize, string username);
         */
    }
}
