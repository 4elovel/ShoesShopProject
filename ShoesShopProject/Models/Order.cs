﻿namespace ShoesShopProject.Models;

public class Order
{
    public int Id { get; set; }
    public List<GoodOrder> GoodOrders { get; set; } = new List<GoodOrder>();
    public ApplicationUser? User { get; set; }
    public OrderStatuses Status { get; set; } = OrderStatuses.Pending;

}
public enum OrderStatuses
{
    Ordered,// process of order is started
    Pending,//pre-order
    Completed,//comleted order
}
