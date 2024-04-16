using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesShopProject.Models;
using ShoesShopProject.ViewModels;

namespace ShoesShopProject.Controllers;

public class ApiController : Controller
{
    private IMapper _mapper;
    private ApplicationContext _context;
    private int pageSize = 8;
    public ApiController(IMapper mapper, ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public ActionResult SearchCards(FindGoodViewModel DTO = null, int page = 0)
    {
        IQueryable<Good> querry = _context.Goods.Include(g => g.GoodPhotos).Include(g => g.Sizes);
        try
        {
            if (DTO != null)
            {
                if (DTO.gender != null) querry = querry.Where(g => g.Gender.ToString() == DTO.gender || g.Gender == 'b');
                if (!string.IsNullOrEmpty(DTO.minPrice) && decimal.Parse(DTO.minPrice) != null) querry = querry.Where(g => g.SalePrice >= Decimal.Parse(DTO.minPrice));
                if (!string.IsNullOrEmpty(DTO.maxPrice) && decimal.Parse(DTO.maxPrice) != null) querry = querry.Where(g => g.SalePrice <= Decimal.Parse(DTO.minPrice));
                if (DTO.sizes != null) querry = querry.Where(g => g.Sizes.Select(g => g.SizeEuro).Any(x => DTO.sizes.Any(y => y == x)));
                if (DTO.categories != null) querry = querry.Where(g => g.Categories.Select(g => g.Name).Any(x => DTO.categories.Any(y => y == x)));
                if (DTO.brands != null) querry = querry.Where(g => DTO.brands.Contains(g.Brand));
                if (DTO.isSaleOnly == "true") querry = querry.Where(g => g.FullPrice != g.SalePrice);
            }
        }
        catch
        {
            return View(new List<GoodCardViewModel>());
        }
        var buf = querry.Skip(page * pageSize).Take(pageSize).ToList();
        var res = _mapper.Map<IEnumerable<GoodCardViewModel>>(buf);
        return View(res);
    }
}
