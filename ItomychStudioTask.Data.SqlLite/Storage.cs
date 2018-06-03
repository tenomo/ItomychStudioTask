using ItomychStudioTask.Data.Abstractions.ModelAbstractions;

namespace ItomychStudioTask.Data.SqlLite
{
  public  class Storage : IStorage
    { 
        public IBookRepository BookRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        public Storage(  IBookRepository bookRepository, ICategoryRepository categoryRepository)
        { 
            BookRepository = bookRepository;
            CategoryRepository = categoryRepository;
        }

       
    }
}