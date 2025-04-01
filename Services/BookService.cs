using BookstoreAPI.Models;
using BookstoreAPI.Repositories;

namespace BookstoreAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public void AddBook(Book book)
        {
            _bookRepository.AddBook(book);
            _bookRepository.Save();
        }

        public void UpdateBook(int id, Book updatedBook)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null) return;

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Price = updatedBook.Price;

            _bookRepository.UpdateBook(book);
            _bookRepository.Save();
        }

        public void DeleteBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null) return;

            _bookRepository.DeleteBook(id);
            _bookRepository.Save();
        }
    }

}


