namespace Euphorolog.UnitTests.Controller
{
    public class UnitTest
    {
        /*
        *** Controller
        => 200 Status
        => Ok Object
        => Expected Value is correct
        => Service called with expected parameter
        => Service called once
        => Service returns an exception then an exception should be raised
        => Multiple test case check
        => Wrapper called once with the value returned from service
        => Return value of wrapper matches
        */
        /* ///Controller Method that needs to be tested///
         * GetAllStoriesAsync([FromQuery] PaginationFilter filter)
         * GetStoryByIdAsync(string id) - done
         * PostStoryAsync(PostStoryRequestDTO story) - done 
         * UpdateStoryAsync([FromRoute] string id, [FromBody] UpdateStoryRequestDTO story) - done
         * DeleteStoryAsync(string id) - done
         * GetStoryByIdDownloadAsync(string id) - done
         */
    }
}