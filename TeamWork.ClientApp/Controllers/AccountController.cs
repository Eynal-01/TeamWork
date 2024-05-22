using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TeamWork.ClientApp.Models;

namespace TeamWork.ClientApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private SignInManager<CustomIdentityUser> _signInManager;
        private IWebHostEnvironment _webHost;
        private CustomIdentityDbContext _context;
        private readonly IPasswordHasher<CustomIdentityUser> _passwordHasher;

        public AccountController(
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

      
         
        
    }
}
