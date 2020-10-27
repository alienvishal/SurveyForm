using System;
using System.Collections;
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

        public List<ListUserViewModel> GetAllResult()
        {
            var result = (from qes in surveyDBContext.Questions
                          join res in surveyDBContext.Results
                          on qes.Q_Id equals res.Q_Id
                          group new {res.SelectedAnswer}
                          by new {qes.Q_Text} into grp
                          orderby grp.Key.Q_Text
                          select new
                          {
                              QText = grp.Key.Q_Text,
                              Answer = grp.Select(x=>x.SelectedAnswer)
                          }).ToList();

            var model = new List<ListUserViewModel>();
            foreach(var res in result)
            {
                ListUserViewModel surveyViewModel = new ListUserViewModel
                {
                    QText = res.QText,
                    SelectedAnswer = res.Answer.ToList()
                };

                model.Add(surveyViewModel);
            }

            return model;
        }

        public bool IsDateChanged(AddFormSubmitDateViewModel model)
        {
            throw new NotImplementedException();
        }

        public List<ListUserViewModel> ListUsers()
        {
            var listUsers = (from user in surveyDBContext.Users
                             join project in surveyDBContext.ProjectInstances
                             on user.project_Id equals project.Project_Id
                             select new
                             {
                                 Name = user.Email,
                                 Project = project.ProjectName,
                                 UserId = user.Id
                             }).ToList();

            var model = new List<ListUserViewModel>();
            foreach(var user in listUsers)
            {
                ListUserViewModel listUserViewModel = new ListUserViewModel
                {
                    Email = user.Name,
                    ProjectInstance = user.Project,
                    Id = user.UserId
                };

                model.Add(listUserViewModel);
            }

            return model;
        }

        
        public bool IsSurveyAdded(SurveyViewModel model)
        {
            int flag = 0;
            for (int i = 0; i < model.Questions.Count; i++)
            {
                Result result = new Result
                {
                    Id = model.UserId,
                    Project_Id = model.ProjectId,
                    Q_Id = model.Questions[i].Q_Id,
                    SelectedAnswer = model.SelectedAnswer[i]
                };
                var res = surveyDBContext.Results.Add(result);
                surveyDBContext.SaveChanges();

                if (res == null)
                    flag = 0;
                else
                    flag = 1;
            }

            if (flag == 0)
                return false;

            return true;
        }

        public bool IsDeleteResult(string userId)
        {
            var result = surveyDBContext.Results
                            .Where(x => x.Id == userId).ToList();
            if (result != null)
            {
                foreach (var res in result)
                {
                    surveyDBContext.Results.Remove(res);
                    surveyDBContext.SaveChanges();
                }
                return true;
            }
            return false;
        }
    }
}
