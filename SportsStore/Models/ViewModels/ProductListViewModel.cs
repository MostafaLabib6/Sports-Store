namespace SportsStore.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        public PagingInfoViewModel PagingInfo { get; set; } = new();
        public string? Category { get; set; }
    }
}
