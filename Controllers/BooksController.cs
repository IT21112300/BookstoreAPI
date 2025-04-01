using Microsoft.AspNetCore.Mvc;
using BookstoreAPI.Models;
using BookstoreAPI.Services;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("booklist")]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return Ok(_bookService.GetAllBooks());
    }

    [HttpPost("addBook")]
    public ActionResult<Book> AddBook(Book book)
    {
        _bookService.AddBook(book);
        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
    }

    [HttpPut("updateBook/{id}")]
    public IActionResult UpdateBook(int id, Book updatedBook)
    {
        var book = _bookService.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }

        _bookService.UpdateBook(id, updatedBook);
        return NoContent();
    }

    [HttpDelete("deleteBook/{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _bookService.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }

        _bookService.DeleteBook(id);
        return NoContent();
    }
}
