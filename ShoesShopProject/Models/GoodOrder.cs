namespace ShoesShopProject.Models;

public class GoodOrder
{
    public int Id { get; set; }
    public int GoodId { get; set; }
    public Good Good { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int Count { get; set; } = 1;
    public int Size { get; set; }
}
