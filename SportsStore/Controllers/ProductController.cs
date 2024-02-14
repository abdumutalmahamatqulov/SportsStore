using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Models;
using SportsStore.Domain.Services;
using SportsStore.Services.Services;

namespace SportsStore.Controllers;

public class ProductController : Controller
{
	readonly IProductService _productService;
	readonly ICategoryService _categoryService;

	public ProductController(IProductService productService, ICategoryService categoryService)
	{
		_productService = productService;
		_categoryService = categoryService;
	}

	public async Task<IActionResult> Index()
	{
		return View(await _productService.GetProductsAsync());
	}
	[HttpGet]
	public async Task<IActionResult> Create(string returnUrl)
	{
		await AddToViewBag();
		ViewBag.ReturnUrl = returnUrl;
		return View(new ProductCreateModel());
	}
	[HttpPost]
	public async Task <IActionResult> Create(ProductCreateModel model, string returnUrl)
	{
		try
		{
			if(!ModelState.IsValid)
			{
				await AddToViewBag();
				return View(model);
			}
			await _productService.Create(model);
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
	private async Task AddToViewBag()
	{
		ViewBag.Categories = await _categoryService.GetCategoriesAsync();
	}
	[HttpPost]
	public async Task <IActionResult> Delete(Guid Id,string returnUrl)
	{
		await _productService.Delete(Id);
		return Redirect(returnUrl);
	}
	[HttpGet]
	public async Task<IActionResult> Update(Guid id, string returnUrl)
	{
		await AddToViewBag();
		ViewBag.ReturnUrl = returnUrl;
		var updateProduct = await  _productService.GetUpdateModel(id);
		return View(updateProduct);
	}
	[HttpPost]
	public async Task<IActionResult> Update(ProductUpdateModel model, string returnUrl)
	{
		try
		{
			if (!ModelState.IsValid)
			{
				await AddToViewBag();
				return View(model);
			}
			await _productService.Update(model);
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
}


