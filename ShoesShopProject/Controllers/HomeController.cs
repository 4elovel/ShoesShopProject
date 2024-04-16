using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesShopProject.Models;
using ShoesShopProject.ViewModels;
using System.Diagnostics;

namespace ShoesShopProject.Controllers
{
    public class HomeController : Controller
    {
        private IMapper _mapper;
        private ApplicationContext _context;
        private int pageSize = 8;
        public HomeController(IMapper mapper, ApplicationContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IActionResult Index()
        {
            //RunDatabase();

            var buf = _context.Goods.Take(pageSize).Include(g => g.GoodPhotos).Include(g => g.Sizes).ToList();
            var res = _mapper.Map<IEnumerable<GoodCardViewModel>>(buf);
            return View(res);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [NonAction]
        private void RunDatabase()
        {
            _context.Goods.Add(new Good() { Name = "Nike air force", Brand = "Nike", Description = "Simple Description", BuyPrice = 500, FullPrice = 2000.50M, Sale = 50, SalePrice = 1000.25M, Gender = 'm' });
            _context.SaveChanges();
            _context.GoodPhotos.Add(new GoodPhoto() { GoodId = 1, Path = "\\images\\four.png", Position = 1 });
            _context.SaveChanges();
            _context.Sizes.Add(new Size() { GoodId = 1, Amount = 4, SizeEuro = "39" });
            _context.Sizes.Add(new Size() { GoodId = 1, Amount = 3, SizeEuro = "40" });
            _context.Sizes.Add(new Size() { GoodId = 1, Amount = 2, SizeEuro = "41" });
            _context.SaveChanges();

            _context.Goods.Add(new Good() { Name = "Nike air force", Brand = "Nike", Description = "Simple Description", BuyPrice = 500, FullPrice = 2000.50M, Sale = 0, SalePrice = 1000.25M, Gender = 'm' });
            _context.SaveChanges();
            _context.GoodPhotos.Add(new GoodPhoto() { GoodId = 2, Path = "\\images\\four.png", Position = 1 });
            _context.SaveChanges();
            _context.Sizes.Add(new Size() { GoodId = 2, Amount = 4, SizeEuro = "39" });
            _context.Sizes.Add(new Size() { GoodId = 2, Amount = 3, SizeEuro = "40" });
            _context.Sizes.Add(new Size() { GoodId = 2, Amount = 2, SizeEuro = "41" });
            _context.SaveChanges();

            _context.Goods.Add(new Good() { Name = "Nike air force", Brand = "Nike", Description = "Simple Description", BuyPrice = 500, FullPrice = 2000.50M, Sale = 0, SalePrice = 2000.50M, Gender = 'm' });
            _context.SaveChanges();
            _context.GoodPhotos.Add(new GoodPhoto() { GoodId = 3, Path = "\\images\\four.png", Position = 1 });
            _context.SaveChanges();
            _context.Sizes.Add(new Size() { GoodId = 3, Amount = 4, SizeEuro = "39" });
            _context.Sizes.Add(new Size() { GoodId = 3, Amount = 3, SizeEuro = "40" });
            _context.Sizes.Add(new Size() { GoodId = 3, Amount = 2, SizeEuro = "41" });
            _context.SaveChanges();

            for (int i = 4; i < 51; i++)
            {
                _context.Goods.Add(new Good() { Name = "Nike air force", Brand = "Nike", Description = "Simple Description", BuyPrice = 500, FullPrice = 2000.50M, Sale = 0, SalePrice = 2000.50M, Gender = 'm' });
                _context.SaveChanges();
                _context.GoodPhotos.Add(new GoodPhoto() { GoodId = i, Path = "\\images\\four.png", Position = 1 });
                _context.SaveChanges();
                _context.Sizes.Add(new Size() { GoodId = i, Amount = 4, SizeEuro = "39" });
                _context.Sizes.Add(new Size() { GoodId = i, Amount = 3, SizeEuro = "40" });
                _context.Sizes.Add(new Size() { GoodId = i, Amount = 2, SizeEuro = "41" });
                _context.SaveChanges();
            }
        }
    }
}
