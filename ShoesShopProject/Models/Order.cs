namespace ShoesShopProject.Models;

public class Order
{
    public int Id { get; set; }
    public List<Good> Goods { get; set; } = [];
    public User? User { get; set; }
    public string Status { get; set; } = "processed";

}
