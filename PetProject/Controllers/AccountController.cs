using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
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

            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(loginModel);
            }

            var Psi = await _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, false);

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

        public IActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangedPasswordModel changedPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var isCurrent = await _userManager.CheckPasswordAsync(user, changedPasswordModel.CurrentPassword);
                if (isCurrent)
                {
                    var result = await _userManager.ChangePasswordAsync(user, changedPasswordModel.CurrentPassword,
                        changedPasswordModel.ChangedPassword);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignOutAsync();
                        ModelState.Clear();
                        ViewBag.IsSuccess = true;
                        return View();
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }   
            } 
            return View(changedPasswordModel);
        }
        
        // private async Task<string> UploadImage(string folderPath, IFormFile image)
        // {
        //     folderPath += Guid.NewGuid().ToString() + "_" + image.FileName;
        //     string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
        //
        //     await image.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
        //     return "/" + folderPath;
        // }
    }
}
