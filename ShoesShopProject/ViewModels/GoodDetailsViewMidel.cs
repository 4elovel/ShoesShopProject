using ShoesShopProject.Models;

namespace ShoesShopProject.ViewModels;

public class GoodDetailsViewMidel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal FullPrice { get; set; }
    public int? Sale { get; set; }
    public decimal? SalePrice { get; set; }
    public string? Slug { get; set; }
    public bool IsInWishlist { get; set; } = false;
    public List<GoodPhoto> GoodPhotos { get; set; } = [];
    public List<Size> Sizes { get; set; } = [];
}
