using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TeamWork.ClientApp.Models;

namespace TeamWork.ClientApp.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private SignInManager<CustomIdentityUser> _signInManager;
        private IWebHostEnvironment _webHost;
        private CustomIdentityDbContext _context;
        private readonly IPasswordHasher<CustomIdentityUser> _passwordHasher;

        public HomeController(
          UserManager<CustomIdentityUser> userManager,
          RoleManager<CustomIdentityRole> roleManager,
          IPasswordHasher<CustomIdentityUser> passwordHasher,
          CustomIdentityDbContext context,
          IWebHostEnvironment webHost,
          SignInManager<CustomIdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _context = context;
            _webHost = webHost;
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
  

                CustomIdentityUser user = new CustomIdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    SeriaNo=model.SeriaNo,
                    BirthDate=model.DateTime,
                    City=model.City,


                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        CustomIdentityRole role = new CustomIdentityRole
                        {
                            Name = "Admin"
                        };

                        IdentityResult roleResult = await _roleManager.CreateAsync(role);
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "We can not add the role!");
                            return View(model);
                        }
                    }

                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                    return RedirectToAction("Login", "Account");

                }
            }

            return View(model);
        }




    }
}
