using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Models;

namespace SportsStore.Controllers;

[Route("[controller]")]
public class AuthController(UserManager<User> userManager,
	SignInManager<User> signInManager,
	RoleManager<IdentityRole<Guid>> roleManager) : Controller
{
	public IActionResult Index()
	{
	
		return View();
	}

	[HttpGet("[action]")]
	public IActionResult Login(string returnUrl)
	{
		ViewBag.ReturnUrl = returnUrl;
		return View(new LoginModel());
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl)
	{
		if (ModelState.IsValid)
		{
			var user = await userManager.FindByEmailAsync(loginModel.Email);
			if (user is null)
			{
				ModelState.AddModelError("", "Email or password is incorrect");
				return View(loginModel);
			}
			var isPasswordValid = await userManager.CheckPasswordAsync(user, loginModel.Password);
			if (!isPasswordValid)
			{
				ModelState.AddModelError("", "Email or password is incorrect");
				return View(loginModel);
			}

			await signInManager.SignInAsync(user, false);
			return Redirect(returnUrl);
		}
		ViewBag.ReturnUrl = returnUrl;
		return View(loginModel);
	}


	[HttpPost("[action]")]
	public async Task<IActionResult> Login2(LoginModel loginModel, string returnUrl)
	{
		var user = await userManager.FindByEmailAsync(loginModel.Email);
		if (user is null)
		{
			ModelState.AddModelError("", "Email or password is incorrect");
			return View(loginModel);
		}

		var result = await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
		if (!result.Succeeded)
		{
			ModelState.AddModelError("", "Email or password is incorrect");
			return View(loginModel);
		}

		return Redirect(returnUrl);
	}
    [HttpGet("[action]")]
    public IActionResult Register(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl ?? "/";

        return View(new RegisterModel());
    }
	[HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterModel registerModel,string returnUrl)
	{
		ViewData["ReturnUrl"] = returnUrl ?? "/";
		if (ModelState.IsValid)
		{
			var user = new User 
			{
				UserName = registerModel.Name, 
				Email = registerModel.Email,
				PasswordHash = registerModel.Password 
			};
			var result = await userManager.CreateAsync(user, registerModel.Password);

			if (result.Succeeded)
			{
				return Redirect(returnUrl);
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		return View(registerModel);
	}
	[HttpPost("[action]")]
	public async ValueTask<IActionResult> Logout(string returnUrl)
	{
		return View(returnUrl);
	}

	private static Dictionary<string, int> _verificstionCodes = new Dictionary<string, int>();

	[HttpGet("[action]")]
    public IActionResult SendCode(string returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View(new SendModel());
    }

    [HttpPost("[action]")]
	public async ValueTask<IActionResult> SendCode([Required]string email,[Required] string returnUrl)
    {
		if (!ModelState.IsValid)
		{
			return View(new SendModel() { Email = email });
		}
		Random random = new Random();
		int code = random.Next(100_000, 999_999);
		_verificstionCodes[email] = code;
		Console.WriteLine($"{email}:  {code}");
		return RedirectToAction(nameof(CheckVerificationCode), new {Email = email, ReturnUrl = returnUrl});
	}
	[HttpGet("[action]")]
	public IActionResult CheckVerificationCode(string email,string returnUrl)
	{
		ViewBag.ReturnUrl = returnUrl;

        return View(new SendModel() { Email = email});
	}
	[HttpPost("[action]")]
    public async ValueTask<IActionResult> CheckVerificationCode(SendModel sendModel, string returnUrl)
    {
        var check = await userManager.FindByEmailAsync(sendModel.Email);
        if (check is null)
        {
            ModelState.AddModelError("", "Email or code is incorrect");
            return View(sendModel);
        }
		var chechcode = _verificstionCodes[sendModel.Email] == sendModel.Code;
		if (!chechcode)
		{
            ModelState.AddModelError("Code", "code is incorrect");
			ViewBag.ReturnUrl = returnUrl;
            return View(sendModel);
        }
		await signInManager.SignInAsync(check, false);
        return RedirectToAction(nameof(NewPassword), new {sendModel.Email, ReturnUrl = returnUrl});
    }
	[HttpGet("[action]")]
	[Authorize]
	public IActionResult NewPassword(string email,string returnUrl)
	{
		ViewBag.ReturnUrl = returnUrl;
		return View(new NewPasswordModel { Email = email});
	}
	[HttpPost("[action]")]
	[Authorize]
    public async ValueTask<IActionResult> NewPassword(NewPasswordModel model, string returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            ModelState.AddModelError("", "User not found");
            return View(model);
        }

        var result = await userManager.RemovePasswordAsync(user);
		result = await userManager.AddPasswordAsync(user, model.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
		await signInManager.SignOutAsync();
        ViewBag.ReturnUrl = returnUrl;
        return Redirect(returnUrl);
    }

}
