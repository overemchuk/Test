using System;
using System.Collections.Generic;
using System.Linq;

using TestApp.Data;

namespace TestApp.Services
{
    public class QuestionService : IQuestionService
    {
        private IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Question> GetAllQuestions()
        {
            var data = _unitOfWork.Questions.ToList();

            return data;
        }

        public IList<Answer> GetQuestionDetails(int questionId)
        {
            var answers = _unitOfWork.Answers.Where(x => x.QuestionId == questionId).ToList();

            return answers;
        }

        public void CloseQuestion(int questionId)
        {
            var question = _unitOfWork.Questions.Get(questionId);

            question.IsClosed = true;

            _unitOfWork.Questions.Update(question);
        }

        public int AddQuestion(string text, string userName)
        {
            var entity = new Question { Text = text, UserName = userName };

            _unitOfWork.Questions.Add(entity);

            return entity.Id;
        }

        public void DeleteQuestion(int questionId)
        {
            var answers = _unitOfWork.Answers.Where(x => x.QuestionId == questionId).ToList();

            foreach (var item in answers)
            {
                _unitOfWork.Answers.Delete(item);
            }

            var question = _unitOfWork.Questions.Get(questionId);

            _unitOfWork.Questions.Delete(question);
        }

        public int AddAnswer(int questionId, string text, string userName)
        {
            var entity = new Answer { QuestionId = questionId, Text = text, UserName = userName };

            _unitOfWork.Answers.Add(entity);

            return entity.Id;
        }

        public Question GetQuestion(int questionId)
        {
            return _unitOfWork.Questions.Get(questionId);
        }

        public void UpdateQuestion(Question question)
        {
            _unitOfWork.Questions.Update(question);
        }
    }
}
