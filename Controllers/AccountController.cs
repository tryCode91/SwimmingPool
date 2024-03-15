using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwimmingPool_V1.Data;
using SwimmingPool_V1.Models;
using SwimmingPool_V1.ViewModels;

namespace SwimmingPool_V1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager; // manager
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context) // dependency injection
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if(user != null) {
                // user is found, check password
                var checkpassword = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                if (checkpassword)
                {
                    
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Pool");
                    }
                }

                //password is incorrect
                TempData["Error"] = "Wrong credentials. Please, try again.";
                return View(loginViewModel);
            }

            // User not found
            TempData["Error"] = "Wrong credentials. Please, try again.";
            return View(loginViewModel);
        }
        
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);

            if (user != null)
            {

                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);

            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
                Age = 32
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

                return RedirectToAction("Login", "Account");
            }
            else
            {
                //TempData["Error"] = 
                newUserResponse.Errors.ToList().ForEach(error => {
                    TempData["Error"] = error.Description;
                });

                return RedirectToAction("Register", "Account");

            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Pool");
        }
    }
}
