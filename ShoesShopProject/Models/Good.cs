using System.ComponentModel.DataAnnotations;

namespace ShoesShopProject.Models;

public class Good
{
    public int Id { get; set; }
    [Display(Name = "назва")]
    public string Name { get; set; } = null!;
    [Display(Name = "бренд")]
    public string? Brand { get; set; } = null!;
    [Display(Name = "опис")]
    public string? Description { get; set; }
    [Display(Name = "закупівельна ціна")]
    public decimal? BuyPrice { get; set; }
    [Display(Name = "повна ціна")]
    public decimal FullPrice { get; set; }
    [Display(Name = "знижка")]
    public int? Sale { get; set; } = 0!;
    [Display(Name = "ціна із знижкою")]
    public decimal SalePrice { get; set; }
    [Display(Name = "слаг")]
    public string Slug { get; set; } = null!;
    [Display(Name = "стать")]
    public char Gender { get; set; } // m-male f-female b-both
    [Display(Name = "категорії")]
    public List<Category> Categories { get; set; } = [];
    [Display(Name = "фото")]
    public List<GoodPhoto> GoodPhotos { get; set; } = [];
    [Display(Name = "розміри")]
    public List<Size> Sizes { get; set; } = [];
    public List<GoodOrder> GoodOrders { get; set; } = new List<GoodOrder>();
}