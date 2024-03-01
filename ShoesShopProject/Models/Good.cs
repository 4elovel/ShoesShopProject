namespace ShoesShopProject.Models;

public class Good
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Brand { get; set; } = null!;
    public string? Description { get; set; }
    public decimal? BuyPrice { get; set; }
    public decimal FullPrice { get; set; }
    public int? Sale { get; set; }
    public decimal? SalePrice { get; set; }
    public string? Slug { get; set; }
    public char Gender { get; set; } // m-male f-female b-both
    public List<Category> Categories { get; set; } = [];
    public List<GoodPhoto> GoodPhotos { get; set; } = [];
    public List<Size> Sizes { get; set; } = [];
}