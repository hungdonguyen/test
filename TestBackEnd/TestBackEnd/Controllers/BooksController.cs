using Microsoft.AspNetCore.Mvc;
using TestBackEnd.Models;
using TestBackEnd.Repositories;

namespace TestBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        // Inject Repository vào Controller
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // 1. GET: api/books (Lấy danh sách sách)
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAll();
            return Ok(books);
        }

        // 2. GET: api/books/5 (Lấy sách theo Id)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // 3. POST: api/books (Thêm sách - Chỉ Admin)
        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Book book)
        {
            var createdBook = await _bookRepository.Add(book);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        // 4. PUT: api/books/5 (Sửa sách - Chỉ Admin)
        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(Book book)
        {
            var updatedBook = await _bookRepository.Update(book);
            if (updatedBook == null)
            {
                return NotFound();
            }
            return Ok(updatedBook);
        }

        // 5. DELETE: api/books/5 (Xoá sách - Chỉ Admin)
        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _bookRepository.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
