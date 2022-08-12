namespace Euphorolog.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 1;
        public int totalPages { get; set; } = 1;
        public int totalRecords { get; set; } = 1;
        public Uri? firstPage { get; set; }
        public Uri? lastPage { get; set; }
        public Uri? nextPage { get; set; }
        public Uri? previousPage { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.data = data;
            this.message = "";
            this.status = "success";
        }
    }
}
