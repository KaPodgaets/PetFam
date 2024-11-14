namespace PetFam.Shared.Models
{
    public class PagedList<T>
    {
        public IReadOnlyList<T> Items { get; set; } = [];
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public bool HasNexPage => PageSize * PageNumber <= TotalCount;
        public bool HasPreviousPage => PageNumber > 1;
    }
}
