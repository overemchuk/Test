using System;
using System.IO;

using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

using TestApp.Data;
using TestApp.Services;

namespace TestApp
{
    public partial class Home : Page
    {
        private const string Success = "success";

        private const string QuestionsDetailsPath = "Views/QuestionDetails.html";

        protected static IQuestionService QuestionService
        {
            get
            {
                return new QuestionService(new XmlUnitOfWork());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetAllQuestions()
        {
            var data = QuestionService.GetAllQuestions();

            return new JavaScriptSerializer().Serialize(data);
        }

        [WebMethod]
        public static string GetQuestionDetails(int questionId)
        {
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, QuestionsDetailsPath);

            var template = File.ReadAllText(templatePath);

            var answers = QuestionService.GetQuestionDetails(questionId);

            return new JavaScriptSerializer().Serialize(new { template, answers });
        }

        [WebMethod]
        public static string CloseQuestion(int questionId)
        {
            var service = QuestionService;

            var question = service.GetQuestion(questionId);

            question.IsClosed = true;

            service.UpdateQuestion(question);

            return Success;
        }

        [WebMethod]
        public static string AddQuestion(string text, string userName)
        {
            var id = QuestionService.AddQuestion(text, userName);

            return id.ToString();
        }

        [WebMethod]
        public static string DeleteQuestion(int questionId)
        {
            QuestionService.DeleteQuestion(questionId);

            return Success;
        }

        [WebMethod]
        public static string AddAnswer(int questionId, string text, string userName)
        {
            var id = QuestionService.AddAnswer(questionId, text, userName);

            return id.ToString();
        }
    }
}