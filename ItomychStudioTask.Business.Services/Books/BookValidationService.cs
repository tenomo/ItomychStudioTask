using System.Linq;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.Business.Services.Books
{
    public class BookValidationService : IBookValidationService
    {
        private readonly IStorage _storage;

        public BookValidationService(IStorage storage)
        {
            _storage = storage;
        }

        public bool IsBookExists(Book book)
        {
            return (_storage.BookRepository.GetAll().Result).Contains(book);

        }

        public bool IsBookBelongsToCategory(Book book)
        {
            return (_storage.CategoryRepository.GetAll().Result).Any(category => category.Id == book.CategoryId);
        }

        public bool IsBookExists(long id)
        {
            return (_storage.BookRepository.GetAll().Result).Any(book => book.Id == id);
        }
    }
}
