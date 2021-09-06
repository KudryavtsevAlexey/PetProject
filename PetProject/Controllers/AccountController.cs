using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetProject.Entities;
using PetProject.Models;

namespace PetProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var user = await _userManager.FindByNameAsync(loginModel.Email);

            if (user==null)
            {
                ModelState.AddModelError("", "User not found");
                return View(loginModel);
            }

            var Psi = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false,false);

            if (Psi.Succeeded)
            {
                return RedirectToAction("MakeTasks", "Home");
            }

            return View(loginModel);

        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = signUpModel.Email,
                    UserName = signUpModel.Email,
                    FirstName = signUpModel.FirstName,
                    LastName = signUpModel.LastName,
                    DateOfBirth = signUpModel.DateOfBirth,
                };
                var result = await _userManager.CreateAsync(user, signUpModel.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("MakeTasks", "Home");
            }
            return View(signUpModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
