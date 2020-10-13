using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using URLWhitelsit.Models;

namespace URLWhitelsit.Controllers
{
    public class SurveyController : Controller
    {
        public IActionResult Survey()
        {
            return View();
        }
    }
}
