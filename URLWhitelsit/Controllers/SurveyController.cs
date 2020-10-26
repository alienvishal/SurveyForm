using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URLWhitelsit.Models;
using URLWhitelsit.ViewModels;

namespace URLWhitelsit.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        private readonly ISurveyRepository repository;

        public SurveyController(ISurveyRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index(string id, int projectId)
        {

            SurveyViewModel model = new SurveyViewModel
            {
                Questions = repository.GetAllQuestion(),
                UserId = id,
                ProjectId = projectId
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SurveyViewModel model)
        {
            if(ModelState.IsValid)
            {
                bool isSurveyAdded = repository.IsSurveyAdded(model);
                if(isSurveyAdded)
                {
                    ViewBag.Success = "Thank you for your time, feedback has been submitted Successfully";
                }
                else
                {
                    ViewBag.Error = "Something Went Wrong";
                }
            }
            return View(model);
        }
    }
}
