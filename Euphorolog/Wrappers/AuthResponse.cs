namespace Euphorolog.Wrappers
{
    public class AuthResponse<T> : Response<T>
    {
        public AuthResponse()
        {
        }
        public AuthResponse(T data) : base(data)
        {
        }
    }
}
