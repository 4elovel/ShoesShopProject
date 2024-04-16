using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesShopProject.Extensions;
using ShoesShopProject.Models;
using ShoesShopProject.ViewModels;

namespace ShoesShopProject.Controllers;
[Authorize]
public class AccountController : Controller
{
    private IMapper _mapper;
    private ApplicationContext _context;
    public AccountController(IMapper mapper, ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public IActionResult Wishlist()
    {
        var user = _context.Users.Where(g => g.Email == User.GetEmail())
                .Include(u => u.WishList).ThenInclude(g => g.GoodPhotos).First();
        var wishlist = user.WishList;
        var DTO = new List<GoodCardViewModel>();
        foreach (var i in wishlist)
        {
            DTO.Add(_mapper.Map<GoodCardViewModel>(i));
        }
        return View(DTO);
    }
    public IActionResult Order()
    {
        var user = _context.Users.Where(g => g.Email == User.GetEmail())
                .Include(u => u.Orders).ThenInclude(o => o.GoodOrders).ThenInclude(go => go.Good).ThenInclude(g => g.GoodPhotos).First();
        var order = user.Orders.Where(o => o.User.Id == user.Id).Where(o => o.Status == OrderStatuses.Pending).FirstOrDefault();
        var goodOrderDTO = new List<GoodOrderViewModel>();
        if (order == null) return View(goodOrderDTO);
        foreach (var i in order.GoodOrders)
        {
            var buf = _mapper.Map<GoodOrderViewModel>(i.Good);
            buf.Size = i.Size;
            buf.Count = i.Count;
            if (i.Good.GoodPhotos.ElementAt(0) != null) buf.Photo = i.Good.GoodPhotos[0].Path;
            goodOrderDTO.Add(buf);
        }
        return View(goodOrderDTO);
    }
    [HttpPost]
    [Route("Account/DeleteGoodFromOrder/{slug}")]
    public IActionResult DeleteGoodFromOrder(string slug)
    {
        try
        {
            var user = _context.Users.Where(g => g.Email == User.GetEmail())
                            .Include(u => u.Orders).ThenInclude(o => o.GoodOrders).ThenInclude(go => go.Good).First();
            var order = user.Orders.Where(o => o.User.Id == user.Id && o.Status == OrderStatuses.Pending).FirstOrDefault();
            order.GoodOrders.Remove(order.GoodOrders.Where(go => go.Good.Slug == slug).First());
            _context.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public IActionResult Order([FromBody] OrderViewModel viewModel)
    {
        try
        {
            viewModel.CountryCode = viewModel.CountryCode.Replace("\"", string.Empty);
            var user = _context.Users.Where(g => g.Email == User.GetEmail())
                                .Include(u => u.Orders).ThenInclude(o => o.GoodOrders).ThenInclude(go => go.Good).First();
            var order = user.Orders.Where(o => o.User.Id == user.Id && o.Status == OrderStatuses.Pending).FirstOrDefault();
            if (order == null || viewModel.Products == null || viewModel.Products.Count <= 0) throw new Exception("No products to order");
            foreach (var i in order.GoodOrders)
            {
                foreach (var j in viewModel.Products)
                {
                    if (i.Good.Slug == j.Slug && i.Size == Int32.Parse(j.Size))
                    {
                        i.Count = Int32.Parse(j.Count);
                    }
                }
            }
            user.PhoneNumber = viewModel.CountryCode + viewModel.PhoneNumber;
            order.Status = OrderStatuses.Ordered;

            _context.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }

    }
}





