using ShoesShopProject.Models;

namespace ShoesShopProject.ViewModels;

public class GoodCardViewModel
{
    public GoodCardViewModel() { }
    public GoodCardViewModel(Good good)
    {
        Id = good.Id;
        Name = good.Name;
        Description = good.Description;
        FullPrice = good.FullPrice;
        SalePrice = good.SalePrice;
        Sale = good.Sale;
        Sizes = good.Sizes;
        GoodPhotos = good.GoodPhotos;
        Slug = good.Slug;
    }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal FullPrice { get; set; }
    public int? Sale { get; set; }
    public decimal? SalePrice { get; set; }
    public string? Slug { get; set; }
    public List<Size> Sizes { get; set; } = [];
    public List<GoodPhoto> GoodPhotos { get; set; } = [];
}
