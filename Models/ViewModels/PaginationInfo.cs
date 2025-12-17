namespace SportsStore.Models.ViewModels;

public class PaginationInfo
{
    public int TotalItems { get; }
    public int PageIndex { get; } // Current page number (1-based)
    public int PageSize { get; }
    
    // The list of ALL elements to display in the UI (including Prev/Next/...)
    public List<PaginationLink> Links { get; set; } = new List<PaginationLink>();

    public PaginationInfo(int totalItems, int pageIndex, int pageSize, int centralLinksCount = 5)
    {
        TotalItems = totalItems;
        PageIndex = pageIndex;
        PageSize = pageSize;
        TruncateLinks(centralLinksCount);
    }
    
    // The list of page numbers to display in the UI (the truncated list)

    // This is the core logic for truncation
    public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);

    private void TruncateLinks(int centralLinksCount)
    {
        // --- 1. PREVIOUS Link ---
        Links.Add(new PaginationLink { Text = "Prev", PageIndex = PageIndex - 1, IsDisabled = PageIndex <= 1 });
        
        // --- 2. THE CORE LOGIC: Truncation ---

        // Always show the first page
        Links.Add(new PaginationLink { Text = "1", PageIndex = 1, IsCurrent = PageIndex == 1 });
        
        // Calculate the central window
        int halfWindow = centralLinksCount / 2;
        int startPage = Math.Max(2, PageIndex - halfWindow);
        int endPage = Math.Min(TotalPages - 1, PageIndex + halfWindow);
        // Adjust window if close to the start or end
        if (PageIndex < halfWindow + 2)
        {
            endPage = Math.Min(TotalPages - 1, centralLinksCount + 1);
        }
        if (PageIndex > TotalPages - (halfWindow + 1))
        {
            startPage = Math.Max(2, TotalPages - centralLinksCount);
        }
        
        // Left Ellipsis Logic (if startPage is far from 2)
        if (startPage > 2)
        {
            Links.Add(new PaginationLink { Text = "...", PageIndex = 0, IsDisabled = true, IsEllipsis = true });
        }

        // Central Page Numbers
        for (int i = startPage; i <= endPage; i++)
        {
            Links.Add(new PaginationLink
            {
                Text = i.ToString(),
                PageIndex = i,
                IsCurrent = PageIndex == i
            });
        }
        
        // Right Ellipsis Logic (if endPage is far from TotalPages - 1)
        if (endPage < TotalPages - 1)
        {
            Links.Add(new PaginationLink { Text = "...", PageIndex = 0, IsDisabled = true, IsEllipsis = true });
        }
        
        // Only show the last page if TotalPages > 1 and it's not the same as the first page
        if (TotalPages > 1 && (Links.LastOrDefault()?.PageIndex != TotalPages))
        {
            Links.Add(new PaginationLink { Text = TotalPages.ToString(), PageIndex = TotalPages, IsCurrent = PageIndex == TotalPages });
        }
        
        // --- 3. NEXT Link ---
        Links.Add(new PaginationLink
        {
            Text = "Next",
            PageIndex = PageIndex + 1,
            IsDisabled = PageIndex >= TotalPages
        });
    }
}