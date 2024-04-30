using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesShopProject.Models;
using ShoesShopProject.ViewModels;

namespace ShoesShopProject.Controllers;
[Authorize(Roles = "Admin")]
public class OrdersController : Controller
{
    ApplicationContext _context;
    public OrdersController(ApplicationContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var orders = _context.Orders.Include(o => o.User).Include(o => o.GoodOrders).ThenInclude(o => o.Good).OrderBy(o => o.Status).ToList();
        var DTO = new List<AdminOrderViewModel>();
        foreach (var order in orders)
        {
            DTO.Add(new AdminOrderViewModel(order));
        }
        return View(DTO);
    }
    public IActionResult Details(string id)
    {
        var order = _context.Orders.Where(o => o.Id == Int32.Parse(id))
            .Include(o => o.GoodOrders).ThenInclude(go => go.Good).Include(o => o.User).FirstOrDefault();

        return View(order);
    }
    [HttpPost]
    public IActionResult ChangeStatus(string id, string status)
    {
        var order = _context.Orders.Where(o => o.Id == Int32.Parse(id)).First();
        order.Status = (OrderStatuses)Int32.Parse(status);
        _context.SaveChanges();
        return Ok();
    }
}
