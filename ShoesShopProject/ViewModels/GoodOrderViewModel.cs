namespace ShoesShopProject.ViewModels;

public class GoodOrderViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal? SalePrice { get; set; }
    public string? Slug { get; set; }
    public int Size { get; set; }
    public int Count { get; set; }
    public string Photo { get; set; } = string.Empty!;
}

