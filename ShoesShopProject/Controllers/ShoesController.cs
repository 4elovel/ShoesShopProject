using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesShopProject.Extensions;
using ShoesShopProject.Models;
using ShoesShopProject.ViewModels;


namespace ShoesShopProject.Controllers;

public class ShoesController : Controller
{
    private IMapper _mapper;
    private ApplicationContext _context;
    private int pageSize = 8;
    public ShoesController(IMapper mapper, ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public IActionResult Find()
    {
        List<string> ViewSizes = new List<string>
            {
                "35",
                "36",
                "37",
                "38",
                "39",
                "40",
                "41",
                "42",
                "43",
                "44",
                "45",
            };
        List<string> ViewBrands = new List<string>
            {
                "Nike",
                "Jordan",
                "Puma",
                "Adidas",
                "New Balance",
                "Timberland",
            };
        List<string> ViewCategories = _context.Categories.Select(g => g.Name).ToList();
        ViewBag.ViewSizes = ViewSizes;
        ViewBag.ViewBrands = ViewBrands;
        ViewBag.ViewCategories = ViewCategories;

        return View(new List<GoodCardViewModel>());
    }
    [HttpPost]
    public IActionResult Find(FindGoodViewModel DTO)
    {
        string buf = DTO.SerializeFieldsToString();
        return Redirect($"/Shoes/Find?{buf}");
    }
    [Route("Shoes/Details/{slug}")]
    public async Task<IActionResult> Details(string slug)
    {
        try
        {
            var good = await _context.Goods.Where(g => g.Slug == slug).Include(g => g.GoodPhotos).Include(g => g.Sizes).FirstOrDefaultAsync();
            if (good == null) return RedirectToAction("Index", "Home");
            var DTO = _mapper.Map<GoodDetailsViewMidel>(good);
            var user = _context.Users.Where(g => g.Email == User.GetEmail()).Include(u => u.WishList).FirstOrDefault();
            if (user != null && user.WishList.Any(g => g.Id == DTO.Id))
            {
                DTO.IsInWishlist = true;
            }

            return View(DTO);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return RedirectToAction("Index", "Home");
        }
    }
    [HttpPost]
    [Route("Shoes/AddToOrder/{id}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> AddToOrder(string id, string size)
    {
        try
        {
            var good = _context.Goods.Where(g => g.Id == Int32.Parse(id)).First();
            if (String.IsNullOrEmpty(size)) return RedirectToAction("Details", "Shoes", new { slug = good.Slug });
            var user = _context.Users.Where(g => g.Email == User.GetEmail())
                .Include(u => u.Orders).ThenInclude(o => o.GoodOrders).ThenInclude(go => go.Good).First();
            var order = user.Orders.Where(o => o.User.Id == user.Id).Where(o => o.Status == OrderStatuses.Pending).FirstOrDefault();
            if (order == null)
            {
                order = new Order
                {
                    Status = OrderStatuses.Pending,
                };
                user.Orders.Add(order);
            }


            var GoodsInOrder = order.GoodOrders.Select(go => go.Good).ToList();
            var goodOrder = order.GoodOrders.FirstOrDefault(go => go.GoodId == good.Id && go.OrderId == order.Id && go.Size == Int32.Parse(size));
            if (goodOrder == null)
            {
                goodOrder = new GoodOrder
                {
                    GoodId = good.Id,
                    OrderId = order.Id,
                    Count = 1,
                    Size = Int32.Parse(size)
                };
                order.GoodOrders.Add(goodOrder);
            }
            else
            {
                goodOrder.Count++;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Shoes", new { slug = good.Slug });
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return RedirectToAction("Index", "Home");
        }
    }
    public IActionResult AddToOrder(string id)
    {
        try
        {
            var good = _context.Goods.Where(g => g.Id == Int32.Parse(id)).FirstOrDefault();
            if (good == null) return RedirectToAction("Index", $"Home");
            return RedirectToAction("Details", "Shoes", new { slug = good.Slug });
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index", $"Home");
        }
    }
    [HttpPost]
    [Route("Shoes/AddToWishlist/{slug}")]
    public IActionResult AddToWishlist(string slug)
    {
        if (!User.IsInRole("User"))
        {
            return Unauthorized();
        }

        try
        {
            var user = _context.Users.Where(g => g.Email == User.GetEmail()).Include(u => u.WishList).First();
            var good = _context.Goods.Where(g => g.Slug == slug).First();

            var trueGood = user.WishList.Where(g => g.Id == good.Id).FirstOrDefault();
            if (trueGood != null)
            {
                user.WishList.Remove(trueGood);
            }
            else
            {
                user.WishList.Add(good);
            }
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
        return Ok();
    }
}

