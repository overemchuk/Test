using System;
using System.IO;

using SharpRepository.Repository;

namespace TestApp.Data
{
    public class XmlUnitOfWork : IUnitOfWork
    {
        private const string DirectoryName = "TestAppData";

        private CustomXmlRepository<Question> _questions;

        private CustomXmlRepository<Answer> _answers;

        private string _directoryDataPath;

        public XmlUnitOfWork()
        {
            Initialize();
        }

        public IRepository<Question, int> Questions
        {
            get { return _questions ?? (_questions = new CustomXmlRepository<Question>(_directoryDataPath)); }
        }

        public IRepository<Answer, int> Answers
        {
            get { return _answers ?? (_answers = new CustomXmlRepository<Answer>(_directoryDataPath)); }
        }

        public void Commit()
        {
            _questions.Save();
            _answers.Save();
        }

        public void Dispose()
        {
        }

        private void Initialize()
        {
            _directoryDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DirectoryName);

            if (!Directory.Exists(_directoryDataPath))
            {
                Directory.CreateDirectory(_directoryDataPath);
            }
        }
    }
}