using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLWhitelsit.ViewModels;

namespace URLWhitelsit.Models
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly SurveyDBContext surveyDBContext;

        public SurveyRepository(SurveyDBContext surveyDBContext)
        {
            this.surveyDBContext = surveyDBContext;
        }
        public List<ProjectInstance> GetAllProjectInstance()
        {
            return surveyDBContext.ProjectInstances.ToList();
        }

        public List<Question> GetAllQuestion()
        {
            return surveyDBContext.Questions.ToList();
        }

        public Question GetQuestionById(int id)
        {
            return surveyDBContext.Questions.Find(id);
        }

        public bool IsLoggedIn(LoginViewModel model)
        {
            var user = surveyDBContext.Users
                     .Where(x => x.Email == model.Email && x.project_Id == model.SelectedInstance)
                     .FirstOrDefault();

            if (user != null)
                return true;

            return false;
        }

        public bool IsQuestionAdded(AddQuestionViewModel model)
        {
            Question question = new Question();
            question.Q_Text = model.UrlText;
            var res = surveyDBContext.Questions.Add(question);
            surveyDBContext.SaveChanges();

            if (res == null)
                return false;

            return true;
        }

        public Question UpdatedQuestion(Question question)
        {
            var res = surveyDBContext.Questions.Attach(question);
            res.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            surveyDBContext.SaveChanges();
            return question;
        }

        public Question DeleteQuestion(int id)
        {
            Question question = surveyDBContext.Questions.Find(id);
            if (question != null)
            {
                surveyDBContext.Questions.Remove(question);
                surveyDBContext.SaveChanges();
            }
            return question;
        }

        public List<Result> GetAllResult()
        {
            throw new NotImplementedException();
        }
    }
}
