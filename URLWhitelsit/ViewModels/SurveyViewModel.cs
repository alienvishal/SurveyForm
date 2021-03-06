﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using URLWhitelsit.Models;

namespace URLWhitelsit.ViewModels
{
    public class SurveyViewModel
    {
        public List<Question> Questions { get; set; }
        public string[] RadioButton = new[] { "Keep", "Discard" };
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public string[] SelectedAnswer { get; set; }
    }
}
