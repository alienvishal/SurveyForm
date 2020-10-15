using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLWhitelsit.Models;
using URLWhitelsit.ViewModels;

namespace URLWhitelsit.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISurveyRepository surveyRepository;
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;

        public AccountController(ISurveyRepository surveyRepository,
                                    UserManager<Users> userManager,
                                    SignInManager<Users> signInManager)
        {
            this.surveyRepository = surveyRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            if(signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Admin"))
                    return RedirectToAction("ListQuestion", "Admin");
                else
                    return RedirectToAction("Index", "Survey");
            }
            ViewBag.ProjectInstance = surveyRepository.GetAllProjectInstance();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                bool isLoggedIn = surveyRepository.IsLoggedIn(model);
                if (isLoggedIn)
                {
                    var loggedIn = await signInManager.PasswordSignInAsync(model.Email.Split("@")[0], "Daimler@123456", isPersistent: false, lockoutOnFailure: false);

                    if (loggedIn.Succeeded)
                    {
                        var user = userManager.FindByEmailAsync(model.Email);
                        if (!string.IsNullOrEmpty(returnUrl))
                            return LocalRedirect(returnUrl);
                        else
                        {
                            if (await userManager.IsInRoleAsync(user.Result, "Admin"))
                                return RedirectToAction("ListQuestion", "Admin");
                            else
                                return RedirectToAction("Index", "Survey");
                        }
                    }
                }
                else
                    ModelState.AddModelError(string.Empty, "Invaild Email Id or Instance number");
            }
            ViewBag.ProjectInstance = surveyRepository.GetAllProjectInstance();
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            LoginViewModel model = new LoginViewModel();
            if (signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Admin"))
                    return RedirectToAction("ListQuestion", "Admin");
                else
                    return RedirectToAction("Index", "Survey");
            }
            ViewBag.ProjectInstance = surveyRepository.GetAllProjectInstance();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new Users() { Email = model.Email, project_Id = model.SelectedInstance, UserName = model.Email.Split("@")[0] };
                var result = await userManager.CreateAsync(user, "Daimler@123456");

                if(result.Succeeded)
                {
                    ViewBag.Message = "Thank you for Registering with us :)";
                    return RedirectToAction("Login", "Account");
                }

                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            ViewBag.ProjectInstance = surveyRepository.GetAllProjectInstance();
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
