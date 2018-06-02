using ItomychStudioTask.Data.Abstractions.ModelAbstractions;

namespace ItomychStudioTask.Tests.DataMock
{
    public class Storage : IStorage
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public Storage(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public void Save()
        {
           
        }

        public IBookRepository BookRepository => _bookRepository;

        public ICategoryRepository CategoryRepository => _categoryRepository;
    }
}
