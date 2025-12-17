namespace SportsStore.Models.ViewModels;

public class PaginationLink
{
    public int PageIndex { get; set; }  // The actual page number, 0 if it's an ellipsis
    public string Text { get; set; }    // The text to display (e.g., "7", "...", "Prev")
    public bool IsCurrent { get; set; }
    public bool IsDisabled { get; set; } // Used for Prev/Next, or if Text is "..."
    public bool IsEllipsis { get; set; } // New flag for the "..." placeholder
}