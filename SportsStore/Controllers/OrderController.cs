using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Services;
using SportsStore.Services.Services;

namespace SportsStore.Controllers;

public class OrderController : Controller
{
    readonly IOrderService _orderService;
    readonly IOrderDetailService _orderDetailService;
	public OrderController(IOrderService orderService, IOrderDetailService orderDetailService = null)
	{
		_orderService = orderService;
		_orderDetailService = orderDetailService;
	}

	public async Task<IActionResult> Index()
	{
		return View(await _orderService.GetOrdersAsync());
	}
    public async Task<IActionResult>  OrderDetail(string detail)
	{
		return View( await _orderDetailService.GetOrderDetailsAsync());

    }
	[HttpPost]
	public async Task<IActionResult> Create(List<Guid> IdList, string returnUrl,string customerName)
	{
		await _orderService.Create(IdList, customerName);
		return Redirect(returnUrl);
	}
	[HttpGet]
	[Route("order/{orderId}/details")]
	public async Task<IActionResult> OrderByDetails(Guid orderId)
	{

		return View(await _orderService.GetByOrderId(orderId));
	}

}
