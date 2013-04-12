using System;

using SharpRepository.Repository;

namespace TestApp.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Question, int> Questions { get; }

        IRepository<Answer, int> Answers { get; }

        void Commit();
    }
}
