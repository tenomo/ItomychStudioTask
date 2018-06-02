namespace ItomychStudioTask.Data.Abstractions.ModelAbstractions
{
    public interface IStorage
    {
        IBookRepository BookRepository { get;   }
        ICategoryRepository CategoryRepository { get;   }
        void Save();
    }
}
