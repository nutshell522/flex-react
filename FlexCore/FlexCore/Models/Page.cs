namespace FlexCore.Models
{
    public class Page<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public IEnumerable<T> Items { get; set; }

        public Page()
        {
            Items = new List<T>();
        }

        public Page(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Items = items;
        }

        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex < TotalPages - 1;
    }
}
