using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Abstractions;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.Tests.DataMock.Repositories
{
   public class BookRepository : IBookRepository
   {
       private   IList<Book> _booksooks;
    
        public void SetStorageContext(IStorageContext storageContext)
        {
            _booksooks = (storageContext as StorageContext).Books;


        }

        public Task<IEnumerable<Book>> GetAll()
        {
          return    new Task<IEnumerable<Book>>(() => _booksooks);
        }

       

        public Task<Book> Get(int id)
        {
          return  new Task< Book>(() => _booksooks.First(book => book.Id  == id));
        }

        public Task Create(Book item)
        {
            
        }

        public Task Update(Book item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

       public Task<IEnumerable<Book>> GetAll(int page, int rows)
       {
           throw new NotImplementedException();
       }
   }
}
