using System.Collections.Generic;
using TestApp.Data;

namespace TestApp.Services
{
    public interface IQuestionService
    {
        int AddAnswer(int questionId, string text, string userName);
        int AddQuestion(string text, string userName);

        void CloseQuestion(int questionId);
        void DeleteQuestion(int questionId);
        void UpdateQuestion(Question question);

        IList<Question> GetAllQuestions();
        IList<Answer> GetQuestionDetails(int questionId);

        Question GetQuestion(int questionId);
    }
}
