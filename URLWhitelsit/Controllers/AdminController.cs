﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "GlobalAdmin, Admin")]
    public class AdminController : Controller
    {
        private readonly ISurveyRepository surveyRepository;
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(ISurveyRepository surveyRepository,
                                UserManager<Users> userManager, 
                                RoleManager<IdentityRole> roleManager)
        {
            this.surveyRepository = surveyRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddQuestion(AddQuestionViewModel model)
        {
            bool isQuestionAdded = surveyRepository.IsQuestionAdded(model);
            if(isQuestionAdded)
            {
                ViewBag.QuestionAdded = "The URL has been Added";
                return View();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Could not able to add URL");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ListQuestion()
        {
            return View(surveyRepository.GetAllQuestion());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var question = surveyRepository.GetQuestionById(id);

            if (question == null)
            {
                ViewBag.Error = $"The id {question.Q_Id} mentioned is not available";
                return View("NotFound");
            }

            EditQuestionViewModel model = new EditQuestionViewModel
            {
                Q_Id = question.Q_Id,
                Q_Text = question.Q_Text
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = surveyRepository.GetQuestionById(model.Q_Id);

                if (res == null)
                {
                    ViewBag.Error = $"The id {res.Q_Id} mentioned is not available";
                    return View("NotFound");
                }

                res.Q_Text = model.Q_Text;
                surveyRepository.UpdatedQuestion(res);
                ViewBag.QuestionEdited = "The URL has been Successfully edited";
            }
            return View(model);
        }
        

        public IActionResult Delete(int id)
        {
            Question delQuestion = surveyRepository.GetQuestionById(id);
            surveyRepository.DeleteQuestion(delQuestion.Q_Id);
            return RedirectToAction("ListQuestion", "Admin");
        }

        [HttpGet]
        public IActionResult AddUserInRole()
        {
            return View(roleManager.Roles.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "GlobalAdmin")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "GlobalAdmin")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole() { Name = model.RoleName };
                var res = await roleManager.CreateAsync(identityRole);

                if (res.Succeeded)
                {
                    ViewBag.RoleCreated = "Role has been Successfully Created";
                }

                foreach (var err in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "GlobalAdmin")]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if(role == null)
            {
                ViewBag.Error = $"The id {role.Id} mentioned is not available";
                return View("NotFound");
            }

            EditRoleViewModel model = new EditRoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            
            foreach(var users in userManager.Users)
            {
                if(await userManager.IsInRoleAsync(users, role.Name))
                {
                    model.Users.Add(users.Email);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GlobalAdmin")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(id);

                if (role == null)
                {
                    ViewBag.Error = $"The id {role.Id} mentioned is not available";
                    return View("NotFound");
                }

                role.Name = model.RoleName;
                IdentityResult res = await roleManager.UpdateAsync(role);

                if (res.Succeeded)
                {
                    ViewBag.RoleEdited = "The Role has been Successfully Updated";
                }

                foreach(var err in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "GlobalAdmin")]
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"The id {role.Id} mentioned is not available";
                return View("NotFound");
            }

            ViewBag.RoleId = roleId;
            var model = new List<AddUserInRoleViewModel>();
            foreach(var user in userManager.Users)
            {
                AddUserInRoleViewModel addUserInRoleViewModel = new AddUserInRoleViewModel
                {
                    UserId = user.Id,
                    Username = user.Email
                };

                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    addUserInRoleViewModel.IsSelected = true;
                }
                else
                {
                    addUserInRoleViewModel.IsSelected = false;
                }

                model.Add(addUserInRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GlobalAdmin")]
        public async Task<IActionResult> AddOrRemoveUser(List<AddUserInRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"The id {role.Id} mentioned is not available";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if(model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if(! model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if(result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        ViewBag.AddOrRemoveUser = "The User in Role has been Successfully Updated";
                }
            }
            return RedirectToAction("EditRole", "Admin", new { id = roleId });
        }

        [HttpGet]
        public ActionResult AddFormClourseDate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFormClourseDate(AddFormSubmitDateViewModel model)
        {
            if(ModelState.IsValid)
            {
                bool isUpdated = surveyRepository.IsDateChanged(model);

                if (isUpdated)
                    ViewBag.DateModified = "Date has been successfully Updated";
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ShowUsers()
        {
            return View(surveyRepository.ListUsers());
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var users = await userManager.FindByIdAsync(id);

            if(users != null)
            {
                var roleForUser = await userManager.GetRolesAsync(users);
                
                if(roleForUser.Count > 0)
                {
                    foreach(var item in roleForUser.ToList())
                    {
                        await userManager.RemoveFromRoleAsync(users, item);
                    }
                }
                surveyRepository.IsDeleteResult(id);
                
                var result = await userManager.DeleteAsync(users);
                if(result.Succeeded)
                {
                    ViewBag.DeletedUser = "The User " + users.Email + " has been successfully deleted";
                    return RedirectToAction("ShowUsers", "Admin");
                }

                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Result()
        {
            return View(surveyRepository.GetAllResult());
        }
    }
}
