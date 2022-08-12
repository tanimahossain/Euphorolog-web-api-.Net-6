namespace Euphorolog.Wrappers
{
    public class StoryResponse<T> : Response<T>
    {
        public StoryResponse()
        {
        }
        public StoryResponse(T data): base(data)
        {

        }
    }
}
