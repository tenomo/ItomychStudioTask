 

using ItomychStudioTask.Data.Abstractions;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;

namespace ItomychStudioTask.Data.SqlLite
{
    class Storage : IStorage
    {
        private readonly StorageContext _context;
        public IBookRepository BookRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        public Storage(StorageContext context , IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            BookRepository = bookRepository;
            CategoryRepository = categoryRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}