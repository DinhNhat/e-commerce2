namespace SportsStore.Models.ViewModels;

public class PaginationInfo
{
    public int TotalItems { get; }
    public int PageIndex { get; } // Current page number (1-based)
    public int PageSize { get; }

    public List<int> PageNumbers { get; } = new List<int>();

    public PaginationInfo(int totalItems, int pageIndex, int pageSize)
    {
        TotalItems = totalItems;
        PageIndex = pageIndex;
        PageSize = pageSize;
        PageNumbers = DeterminePageNum();
    }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    
    // The list of page numbers to display in the UI (the truncated list)

    // This is the core logic for truncation
    public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);

    public List<int> DeterminePageNum()
    {
        int maxPageLinks = 7;
        // Logic to determine which page numbers to display
        // We want to show a few pages around the current page (e.g., 3 before, current, 3 after)

        int startPage = Math.Max(1, PageIndex - (maxPageLinks / 2));
        int endPage = Math.Min(TotalPages, PageIndex + (maxPageLinks / 2));
        
        if (TotalPages > maxPageLinks)
        {
            if (endPage == TotalPages)
            {
                startPage = Math.Max(1, TotalPages - maxPageLinks + 1);
            }
            else if (startPage == 1)
            {
                endPage = Math.Min(TotalPages, maxPageLinks);
            }
        }
        
        for (int i = startPage; i <= endPage; i++)
        {
            PageNumbers.Add(i);
        }
        // You can add more complex logic to show '1...' and '...TotalPages'
        return PageNumbers;
    }
}