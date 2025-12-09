using Microsoft.EntityFrameworkCore;
using TestBackEnd.Models;

namespace TestBackEnd.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context; // Nên để readonly

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public  async Task<Book> Add(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<List<Book>> GetByGenreId(int genreId)
        {
            var books = _context.Books.Where(b => b.GenreId == genreId);
            return await books.ToListAsync();
        }

        public async Task<Book?> GetById(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book?> Update(Book book)
        {
            var existingBook = await _context.Books.FindAsync(book.Id);

            if (existingBook == null)
            {
                return null;
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Price = book.Price;
            existingBook.GenreId = book.GenreId;
            existingBook.StockQuantity = book.StockQuantity;


            await _context.SaveChangesAsync();
            return book;
        }
    }
}
