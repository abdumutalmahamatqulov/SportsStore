using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Models;
using SportsStore.Domain.Services;

namespace SportsStore.Controllers;

[Route("[controller]")]
public class CategoryController : Controller
{
	readonly ICategoryService _categoryService;

	public CategoryController(ICategoryService categoryService)
	{
		_categoryService = categoryService;
	}

	[HttpGet("[action]")]
	public async  Task<IActionResult> Index()
	{
		return View(await _categoryService.GetCategoriesAsync());
	}
	[HttpGet("[action]")]
	public async Task<IActionResult> Create(string returnUrl)
	{
		await AddToViewBag();
		ViewBag.ReturnUrl = returnUrl;
		return View(new CategoryCreateModel());
	}
	[HttpPost("[action]")]
	public async Task<IActionResult> Create(CategoryCreateModel model,string returnUrl)
	{
		try
		{
			if (!ModelState.IsValid)
			{
				await AddToViewBag();
				return View(model);
			}
			await _categoryService.Create(model);
			return Redirect(returnUrl);
		}
		catch (Exception ex)
		{
			await AddToViewBag();
			ViewBag.ReturnUrl = returnUrl;
			ModelState.AddModelError(string.Empty, ex.Message);
			return View(model);
		}
	}
	[HttpGet("[action]")]
	public async Task<IActionResult> Update(Guid id,string returnUrl)
	{
		await AddToViewBag();
		ViewBag.ReturnUrl = returnUrl;
		var updateCategory = await _categoryService.GetUpdateModel(id);
		return View(updateCategory);
	}
	[HttpPost("[action]")]
	public async Task<IActionResult> Update(CategoryUpdateModel model,string returnUrl)
	{
		try
		{
			if (!ModelState.IsValid)
			{
				await AddToViewBag();
				return View(model);
			}
			await _categoryService.Update(model);
			return Redirect(returnUrl);
		}
		catch (Exception ex)
		{
			await AddToViewBag();
			ViewBag.ReturnUrl = returnUrl;
			ModelState.AddModelError(returnUrl, ex.Message);
			return View(model);
		}
	}

	[HttpPost("[action]")]
	public async  Task<IActionResult> Delete(Guid id,string returnUrl)
	{
		await _categoryService.Delete(id);
		return Redirect(returnUrl);
	}
	private async Task AddToViewBag()
	{
		ViewBag.Categories = await _categoryService.GetCategoriesAsync();
	}
}
