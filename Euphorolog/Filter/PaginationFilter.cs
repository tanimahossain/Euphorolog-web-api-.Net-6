﻿namespace Euphorolog.Filter
{
    public class PaginationFilter
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public PaginationFilter()
        {
            this.pageNumber = 1;
            this.pageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.pageSize = pageSize > 10 ? 10 : pageSize;
            this.pageSize = pageSize < 1 ? 1 : this.pageSize;
        }
    }
}
