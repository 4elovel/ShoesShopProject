namespace ShoesShopProject.Models;

public class Size
{
    public int Id { get; set; }
    public int GoodId { get; set; }
    public Good Good { get; set; } = null!;
    public double SizeEuro { get; set; }
    public int Amount { get; set; }
}
