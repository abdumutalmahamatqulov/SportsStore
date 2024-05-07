using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SportsStore.Domain.Models;  
using Microsoft.EntityFrameworkCore;
using SportsStore.Domain.Entities;
using SportsStore.Data.Repositories.EntityFrameworkRepositories;

namespace SportsStore.Controllers;

[Route("[controller]")]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly AppDbContext _appDbContext;
    public UserController(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, AppDbContext appDbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _appDbContext = appDbContext;
    }
    [HttpGet("[action]")]
    public async ValueTask<IActionResult> Index(UserModel model)
    {
		
		var registeredUsers = (await _userManager.Users.ToListAsync()).Select(x=> new UserModel().MapFromEntity(x)).ToList();

        return View(registeredUsers);
    }
    private async Task AddToViewBag()
    {
        ViewBag.Roles =( await _roleManager.Roles.ToListAsync()).Select(x=> new RoleModel().MapFromEntity(x)).ToList();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> UserProfil(Guid userId,string returnUrl)
    {
        await AddToViewBag();
        ViewBag.ReturnUrl = returnUrl;
        var user = await _userManager.Users.Include(x=>x.Roles)
            .ThenInclude(x=>x.Role)
            .FirstOrDefaultAsync(x => x.Id == userId);
        var usermodel =  new UserModel().MapFromEntity(user);

        return View(usermodel);

    }
    [HttpPost("[action]")]
    public async Task<IActionResult> UserProfil(UserRoleCreateModel model,string returnUrl)
    {
        await AddToViewBag();
        ViewBag.ReturnUrl = returnUrl;
        var user = await _userManager.Users.Include(x => x.Roles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == model.UserId);
        if (user == null)
        {
            ModelState.AddModelError("UserId", "user is not found");
        }
        var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
        if(role == null)
        {
            ModelState.AddModelError("RoleId", "Role is not found");
        }

        var isInRole = user.Roles
            .Any(y => y.RoleId == model.RoleId);
        if (isInRole)
        {
            ModelState.AddModelError("RoleId", "Role is already added to this User");
        }
        try
        {
            if (!ModelState.IsValid)
            {
                
                return View(new UserModel().MapFromEntity(user));
            }
        }
        catch (Exception ex)
        {
            await AddToViewBag();
            ViewBag.ReturnUrl = returnUrl;
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }


         _appDbContext.UserRoles.Add(new UserRole() { Id = Guid.NewGuid(),RoleId = model.RoleId,UserId=model.UserId});
        await _appDbContext.SaveChangesAsync();
        var usermodel = new UserModel().MapFromEntity(user);
        return View(usermodel);
    }
}
