namespace SportsStore.Models.ViewModels;

public class PagingInfo
{
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int CurrentPage { get; set; }
    

    public int StartPage { get; private set; }
    public int EndPage { get; private set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
}