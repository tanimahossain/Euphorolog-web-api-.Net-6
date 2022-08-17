namespace Euphorolog.Wrappers
{
    public class UserResponse<T> : Response<T>
    {
        public UserResponse()
        {
        }
        public UserResponse(T data) : base(data)
        {
        }
    }
}
