using Euphorolog.Filter;
using Euphorolog.Services.Contracts;
using Euphorolog.Wrappers;

namespace Euphorolog.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedResponse<List<T>>(pagedData, validFilter.pageNumber, validFilter.pageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.pageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            PaginationFilter validFilterNextPage = new(validFilter.pageNumber + 1, validFilter.pageSize);
            respose.nextPage =
                validFilter.pageNumber >= 1 && validFilter.pageNumber < roundedTotalPages
                ? uriService.GetPageUri(validFilterNextPage.pageNumber, validFilterNextPage.pageSize, route)
                : null;

            PaginationFilter validFilterPreviousPage = new(validFilter.pageNumber - 1, validFilter.pageSize);
            respose.previousPage =
                validFilter.pageNumber - 1 >= 1 && validFilter.pageNumber <= roundedTotalPages
                ? uriService.GetPageUri(validFilterPreviousPage.pageNumber, validFilterPreviousPage.pageSize, route)
                : null;

            PaginationFilter validFilterFirstPage = new(1, validFilter.pageSize);
            respose.firstPage = uriService.GetPageUri(validFilterFirstPage.pageNumber, validFilterFirstPage.pageSize, route);
            PaginationFilter validFilterLastPage = new(roundedTotalPages, validFilter.pageSize);
            respose.lastPage = uriService.GetPageUri(validFilterLastPage.pageNumber, validFilterLastPage.pageSize, route);
            respose.totalPages = roundedTotalPages;
            respose.totalRecords = totalRecords;
            return respose;
        }
    }
}
