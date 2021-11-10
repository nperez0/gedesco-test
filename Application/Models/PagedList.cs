using System.Collections.Generic;

namespace Application.Models
{
    public class PagedList<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages => (TotalItems / PageSize) + 1;

        public IReadOnlyCollection<T> Items { get; set; }
    }
}
