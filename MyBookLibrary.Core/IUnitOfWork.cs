using System;
using System.Threading.Tasks;
using MyBookLibrary.Core.Repositories;

namespace MyBookLibrary.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        Task<int> CommitAsync();
    }
}