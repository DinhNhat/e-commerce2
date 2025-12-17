namespace SportsStore.Models.ViewModels;

public class ProductsListViewModel
{
    public IEnumerable<Product> Products { get; }
    
    // public PagingInfo PagingInfo { get; set; } = new();

    public PaginationInfo PaginationInfo { get; }

    public string? CurrentCategory { get; }

    public ProductsListViewModel(IEnumerable<Product>  products, PaginationInfo pageInfo,  string? currentCategory = null)
    {
        Products = products;
        PaginationInfo = pageInfo;
        CurrentCategory = currentCategory;
    }
    
}