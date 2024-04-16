using ShoesShopProject.Models;

namespace ShoesShopProject.ViewModels;

public class AdminOrderViewModel
{
    public AdminOrderViewModel(Order order)
    {
        OrderId = order.Id;
        Status = order.Status;
        PhoneNumber = order.User.PhoneNumber;
        TotalPrice = 0;
        foreach (var i in order.GoodOrders)
        {
            TotalPrice += i.Good.SalePrice * (decimal)i.Count;
        }
    }
    public int OrderId { get; set; }
    public OrderStatuses Status { get; set; }
    public decimal TotalPrice { get; set; }
    public string PhoneNumber { get; set; }
}
