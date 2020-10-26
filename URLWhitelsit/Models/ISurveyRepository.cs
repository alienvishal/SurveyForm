using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLWhitelsit.ViewModels;

namespace URLWhitelsit.Models
{
    public interface ISurveyRepository
    {
        bool IsQuestionAdded(AddQuestionViewModel model);
        List<ProjectInstance> GetAllProjectInstance();
        bool IsLoggedIn(LoginViewModel model);
        List<Question> GetAllQuestion();
        Question GetQuestionById(int id);
        Question UpdatedQuestion(Question question);
        Question DeleteQuestion(int id);
        List<Result> GetAllResult();
        bool IsDateChanged(AddFormSubmitDateViewModel model);
        List<ListUserViewModel> ListUsers();
        bool IsSurveyAdded(SurveyViewModel model);
    }
}
