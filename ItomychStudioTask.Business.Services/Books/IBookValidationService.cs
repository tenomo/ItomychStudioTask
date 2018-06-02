using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.Business.Services.Books
{
    public interface IBookValidationService
    {
        bool IsBookExists(Book book    );
        bool IsBookExists(long id);
        bool IsBookBelongsToCategory(Book book);
    }
}