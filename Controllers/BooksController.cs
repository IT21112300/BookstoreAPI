using Microsoft.AspNetCore.Mvc;
using BookstoreAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("booklist")]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return _context.Books.ToList();
    }

    [HttpPost("addBook")]
    public ActionResult<Book> AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
    }

    [HttpPut("updateBook/{id}")]
    public IActionResult UpdateBook(int id, Book updatedBook)
    {
        var book = _context.Books.Find(id);
        if (book == null)
        {
            return NotFound();
        }

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.Price = updatedBook.Price;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("deleteBook/{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        _context.SaveChanges();
        return NoContent();
    }

}
