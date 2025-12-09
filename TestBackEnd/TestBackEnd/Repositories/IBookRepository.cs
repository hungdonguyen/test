using TestBackEnd.Models;

namespace TestBackEnd.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll();
        Task<Book?> GetById(int id);
        Task<Book>Add(Book book);
        Task<Book?> Update(Book book);
        Task<bool> Delete(int id);
        Task<List<Book>> GetByGenreId(int genreId);
    }
}