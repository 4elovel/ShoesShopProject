namespace ShoesShopProject.ViewModels;

public class OrderViewModel
{
    public string CountryCode { get; set; }
    public string PhoneNumber { get; set; }
    public List<ProductViewModel> Products { get; set; }
}
