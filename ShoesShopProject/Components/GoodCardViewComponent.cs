using Microsoft.AspNetCore.Mvc;
using ShoesShopProject.ViewModels;

namespace ShoesShopProject.Components;
[ViewComponent]
public class GoodCardViewComponent : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync(GoodCardViewModel goodCardViewModel)
	{
		return View(goodCardViewModel);
	}
}
