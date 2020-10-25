using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using URLWhitelsit.Models;
using URLWhitelsit.ViewModels;

namespace URLWhitelsit.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyRepository repository;

        public SurveyController(ISurveyRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            SurveyViewModel model = new SurveyViewModel
            {
                Questions = repository.GetAllQuestion()
            };
            return View(model);
        }
    }
}
