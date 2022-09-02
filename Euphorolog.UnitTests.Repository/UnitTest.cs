namespace Euphorolog.UnitTests.Repository
{
    public class UnitTest
    {
        /*
        *** Repository
        => Expected Value is correct
        => Expected type match
        => context called with expected parameter and context called once
        => context returns an exception then an exception should be raised
        => Multiple test case check
        */
        /* ///Repository Method that needs to be tested/// (couldn't throw exception from database for some of them)
         * <bool> DeleteStoryAsync(string id) - done
         * <int> GetTotalStoryCountAsync() - done 
         * <int> GetTotalStoryCountOfAUserAsync(string username) - done
         * <int> GetMaxStoryNoByUserId(string id) - done
         * <List<Stories>> GetAllStoriesAsync(int pageNumber, int pageSize) - done
         * <List<Stories>> GetStoriesByUserIdAsync(int pageNumber, int pageSize, string username) - done
         * <Stories?> GetStoryByIdAsync(string id) - done
         * <Stories> PostStoryAsync(Stories story) - done
         * <Stories> UpdateStoryAsync(string id, Stories story) - done
         */
    }
}