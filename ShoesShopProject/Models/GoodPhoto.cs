namespace ShoesShopProject.Models;

public class GoodPhoto
{
    public int Id { get; set; }
    public int GoodId { get; set; }
    public Good Good { get; set; } = null!;
    public string Path { get; set; } = null!;
    public int Position { get; set; }
}
