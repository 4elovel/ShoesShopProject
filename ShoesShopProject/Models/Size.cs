namespace ShoesShopProject.Models;

public class Size
{
    public int Id { get; set; }
    public int GoodId { get; set; }
    public Good Good { get; set; } = null!;
    public string SizeEuro { get; set; } = string.Empty;
    public int Amount { get; set; }
}
