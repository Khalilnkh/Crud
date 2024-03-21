using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P335_BackEnd.Areas.Admin.Models;
using P335_BackEnd.Entities;

namespace P335_BackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            if (!ModelState.IsValid) return View(model);

            var existingUser = await _userManager.FindByNameAsync(model.Email);

            if(existingUser is null)
            {
                model.ErrorMessage = "Username or password is incorrect!";
                return View(model);
            }

            var result = await _signInManager
                .PasswordSignInAsync(existingUser, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                model.ErrorMessage = "Username or password is incorrect!";
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            var roles = _roleManager.Roles.ToList();

            var model = new AccountRegisterVM
            {
                Roles = roles
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var existingRole = await _roleManager.FindByIdAsync(model.RoleId);
            if(existingRole is null)
            {
                model.ErrorMessage = "Role does not exist!";
                return View(model);
            }

            var newUser = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };

            var result =  await _userManager.CreateAsync(newUser, model.Password);
            await _userManager.AddToRoleAsync(newUser, existingRole.Name);

            if (!result.Succeeded)
            {
                model.ErrorMessage = string.Join(" ", result.Errors.Select(x => x.Description));
                return View(model);
            }

            Console.WriteLine(result.ToString());
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}