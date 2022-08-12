namespace Euphorolog.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T Data)
        {
            status = "success";
            message = string.Empty;
            data = Data;

        }
        public T data { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}
