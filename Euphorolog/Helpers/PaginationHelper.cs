using Euphorolog.Filter;
using Euphorolog.Services.Contracts;
using Euphorolog.Wrappers;

namespace Euphorolog.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter validFilter, int totalRecords)
        {
            var respose = new PagedResponse<List<T>>(pagedData, validFilter.pageNumber, validFilter.pageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.pageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.totalPages = roundedTotalPages;
            respose.totalRecords = totalRecords;
            return respose;
        }
    }
}
