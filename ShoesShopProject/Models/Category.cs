namespace ShoesShopProject.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Slug { get; set; } = null!;
    public List<Good> Goods { get; set; } = [];
}
