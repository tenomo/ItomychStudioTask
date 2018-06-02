using System.Collections.Generic;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.Business.Services.Books
{
   public class BookService : IBookService
    {
        private readonly IStorage _storage;
        private readonly IBookValidationService _bookValidationService;

        public BookService(IStorage storage, IBookValidationService bookValidationService)
       {
           _storage = storage;
           _bookValidationService = bookValidationService;
       }

       public Task<IEnumerable<Book>> GetAll()
       {
          return _storage.BookRepository.GetAll();
       }
        public Task<Book> Get(long id)
        {
           
            return _storage.BookRepository.Get(id);
        }
        public Task Create(Book book)
        {
            return _storage.BookRepository.Create(book);
        }
        public Task  Update(Book book)
        {
            return _storage.BookRepository.Update(book);
        }
        public Task Delete(long id)
        {
            return _storage.BookRepository.Delete(id);
        }

        public Task<IEnumerable<Book>> GetAll(int page, int rows)
        {
            return _storage.BookRepository.GetAll(page,rows);
        }

        public IBookValidationService BookValidationService => _bookValidationService;
    }
}
