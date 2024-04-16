using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesShopProject.Infrastructure;
using ShoesShopProject.Models;
using Slugify;
namespace ShoesShopProject.Controllers;

[Authorize(Roles = "Admin")]
public class GoodsController : Controller
{
    private ApplicationContext _context;
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

    public GoodsController(ApplicationContext context)
    {
        _context = context;
    }

    // GET: Goods

    public async Task<IActionResult> Index()
    {
        return View(await _context.Goods.ToListAsync());
    }

    // GET: Goods/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var good = await _context.Goods
            .Include(g => g.Sizes)
            .Include(g => g.GoodPhotos)
            .Include(g => g.Categories)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (good == null)
        {
            return NotFound();
        }

        return View(good);
    }

    // GET: Goods/Create
    public IActionResult Create()
    {
        ViewBag.ViewSizes = ViewSizes;
        return View();
    }

    // POST: Goods/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Brand,Description,BuyPrice,FullPrice,Sale,SalePrice,Slug,Gender")] Good good,
        [Bind("sizes")] List<string> sizes,
        [Bind("sizesCounts")] List<string> sizesCounts,
        [Bind("categories")] List<string> categories,
        [Bind("file1")] IFormFile file1,
        [Bind("file2")] IFormFile file2,
        [Bind("file3")] IFormFile file3,
        [Bind("file4")] IFormFile file4,
        [Bind("file5")] IFormFile file5)
    {
        try
        {
            SlugHelper slugHelper = new SlugHelper();
            if (good.Slug != null && good.Slug != "") good.Slug = slugHelper.GenerateSlug(good.Slug);
            else
            {
                var slug = good.Name;
                good.Slug = slugHelper.GenerateSlug(slug);
            }


            List<string> ViewCategories = _context.Categories.Select(g => g.Name).ToList();

            List<IFormFile> photos = new List<IFormFile>() { file1, file2, file3, file4, file5 };
            var localPhotoPath = Path.Combine("wwwroot", "images", "goods");
            var databasePhotoPath = "\\images\\goods\\";

            for (int i = 0; i < photos.Count; i++)
            {
                if (photos[i] != null)
                {
                    var guid = Guid.NewGuid();
                    good.GoodPhotos.Add(new GoodPhoto { Path = $"{databasePhotoPath}{guid}.png" });
                    using (var stream = new FileStream(Path.Combine(localPhotoPath, $"{guid}.png"), FileMode.Create))
                    {
                        photos[i].CopyTo(stream);
                    }
                    if (good.GoodPhotos.Count > 1)
                    {
                        good.GoodPhotos.Last().Position = good.GoodPhotos[good.GoodPhotos.Count - 2].Position + 1;
                    }
                    else
                    {
                        good.GoodPhotos.Last().Position = 1;
                    }
                }
            }

            foreach (var i in sizes)
            {
                good.Sizes.Add(new Size() { SizeEuro = i, Amount = Int32.Parse(sizesCounts[Int32.Parse(i) - Int32.Parse(ViewSizes[0])]) });
            }

            foreach (var i in categories)
            {
                if (!String.IsNullOrEmpty(i))
                {
                    Category cat = null!;
                    if (ViewCategories.Contains(i)) cat = _context.Categories.Where(c => c.Name == i).First();
                    else if (ViewCategories.Contains(i.FirstCharToLowerCase())) cat = _context.Categories.Where(c => c.Name == i.FirstCharToLowerCase()).First();
                    else if (ViewCategories.Contains(i.FirstCharToUpperCase())) cat = _context.Categories.Where(c => c.Name == i.FirstCharToUpperCase()).First();
                    else
                    {
                        good.Categories.Add(new Category() { Name = i });
                        continue;
                    }
                    good.Categories.Add(cat);
                }
            }

            _context.Add(good);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return View(good);
        }
    }

    // GET: Goods/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {


        ViewBag.ViewSizes = ViewSizes;

        if (id == null)
        {
            return NotFound();
        }

        var good = await _context.Goods.Where(g => g.Id == id).Include(g => g.Sizes).Include(g => g.Categories).Include(g => g.GoodPhotos).FirstOrDefaultAsync();
        if (good == null)
        {
            return NotFound();
        }
        return View(good);
    }

    // POST: Goods/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,Description,BuyPrice,FullPrice,Sale,SalePrice,Slug,Gender")] Good good,
        [Bind("sizes")] List<string> sizes,
        [Bind("sizesCounts")] List<string> sizesCounts,
        [Bind("categories")] List<string> categories,
        [Bind("file1")] IFormFile file1,
        [Bind("file2")] IFormFile file2,
        [Bind("file3")] IFormFile file3,
        [Bind("file4")] IFormFile file4,
        [Bind("file5")] IFormFile file5)
    {
        List<IFormFile> photos = new List<IFormFile>() { file1, file2, file3, file4, file5 };
        if (id != good.Id)
        {
            return NotFound();
        }


        try
        {
            var DBgood = _context.Goods.Where(g => g.Id == good.Id).Include(g => g.Sizes).Include(g => g.Categories).Include(g => g.GoodPhotos).FirstOrDefault();
            var localPhotoPath = Path.Combine("wwwroot", "images", "goods");
            var databasePhotoPath = "\\images\\goods\\";
            List<string> ViewCategories = _context.Categories.Select(g => g.Name).ToList();

            for (int i = 0; i < photos.Count; i++)
            {
                if (DBgood.GoodPhotos.Count > i && photos[i] == null)
                {
                    good.GoodPhotos.Add(DBgood.GoodPhotos[i]);
                    continue;
                }
                if (DBgood.GoodPhotos.Count > i && photos[i] != null)
                {
                    var fileToDelPath = $"wwwroot\\{DBgood.GoodPhotos[i].Path}";
                    if (System.IO.File.Exists(fileToDelPath))
                    {
                        System.IO.File.Delete(fileToDelPath);
                    }

                    good.GoodPhotos.Add(DBgood.GoodPhotos[i]);
                    var guid = Guid.NewGuid();
                    using (var stream = new FileStream(Path.Combine(localPhotoPath, $"{guid}.png"), FileMode.Create))
                    {
                        photos[i].CopyTo(stream);
                    }
                    good.GoodPhotos.Last().Path = $"{databasePhotoPath}{guid}.png";
                    continue;
                }
                if (photos[i] != null)
                {
                    var guid = Guid.NewGuid();
                    good.GoodPhotos.Add(new GoodPhoto { Path = $"{databasePhotoPath}{guid}.png" });
                    using (var stream = new FileStream(Path.Combine(localPhotoPath, $"{guid}.png"), FileMode.Create))
                    {
                        photos[i].CopyTo(stream);
                    }
                    if (good.GoodPhotos.Count <= 1)
                    {
                        good.GoodPhotos.Last().Position = 1;
                        continue;
                    }
                    good.GoodPhotos.Last().Position = good.GoodPhotos[good.GoodPhotos.Count - 2].Position + 1;

                }
            }

            foreach (var i in sizes)
            {
                bool leaver = true;
                foreach (var j in DBgood.Sizes)
                {
                    if (j.SizeEuro == i)
                    {
                        good.Sizes.Add(j);
                        good.Sizes.Last().Amount = Int32.Parse(sizesCounts[Int32.Parse(i) - Int32.Parse(ViewSizes[0])]);
                        leaver = false;
                    }
                }
                if (leaver) good.Sizes.Add(new Size() { SizeEuro = i, Amount = Int32.Parse(sizesCounts[Int32.Parse(i) - Int32.Parse(ViewSizes[0])]) });
            }

            foreach (var i in categories)
            {
                bool leaver = true;
                foreach (var j in DBgood.Categories)
                {
                    if (j.Name.ToLower() == i.ToLower())
                    {
                        good.Categories.Add(j);
                        leaver = false;
                    }
                }
                if (!String.IsNullOrEmpty(i))
                {
                    Category cat = null!;
                    var categoriesNames = good.Categories.Select(c => c.Name.ToLower()).ToList();
                    if (ViewCategories.Contains(i) && !categoriesNames.Contains(i.ToLower()))
                    {
                        cat = _context.Categories.Where(c => c.Name == i).First();
                        good.Categories.Add(cat);
                    }
                    else if (ViewCategories.Contains(i.FirstCharToLowerCase()) && !categoriesNames.Contains(i.ToLower()))
                    {
                        cat = _context.Categories.Where(c => c.Name == i.FirstCharToLowerCase()).First();
                        good.Categories.Add(cat);
                    }
                    else if (ViewCategories.Contains(i.FirstCharToUpperCase()) && !categoriesNames.Contains(i.ToLower()))
                    {
                        cat = _context.Categories.Where(c => c.Name == i.FirstCharToUpperCase()).First();
                        good.Categories.Add(cat);
                    }
                    else if (!categoriesNames.Contains(i.ToLower()) && leaver)
                    {
                        good.Categories.Add(new Category() { Name = i });
                    }

                }
            }

            DBgood.Name = good.Name;
            DBgood.Brand = good.Brand;
            DBgood.Description = good.Description;
            DBgood.BuyPrice = good.BuyPrice;
            DBgood.FullPrice = good.FullPrice;
            DBgood.Sale = good.Sale;
            DBgood.SalePrice = good.SalePrice;
            DBgood.Slug = good.Slug;
            DBgood.Gender = good.Gender;
            DBgood.Sizes = good.Sizes;
            DBgood.Categories = good.Categories;
            DBgood.GoodPhotos = good.GoodPhotos;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GoodExists(good.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return RedirectToAction(nameof(Edit));
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Goods/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var good = await _context.Goods
            .FirstOrDefaultAsync(m => m.Id == id);
        if (good == null)
        {
            return NotFound();
        }

        return View(good);
    }

    // POST: Goods/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var good = await _context.Goods.Include(g => g.GoodPhotos).Where(g => g.Id == id).FirstAsync();
        if (good != null)
        {
            for (int i = 0; i < good.GoodPhotos.Count; i++)
            {
                var fileToDelPath = $"wwwroot\\{good.GoodPhotos[i].Path}";
                if (System.IO.File.Exists(fileToDelPath))
                {
                    System.IO.File.Delete(fileToDelPath);
                }
            }
            _context.Goods.Remove(good);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GoodExists(int id)
    {
        return _context.Goods.Any(e => e.Id == id);
    }
}
