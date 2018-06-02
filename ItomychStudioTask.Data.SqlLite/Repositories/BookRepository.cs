using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Abstractions;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ItomychStudioTask.Data.SqlLite.Repositories
{
    class BookRepository : IBookRepository
    {
        private StorageContext storageContext;
        private DbSet<Book> bookSet;

        public BookRepository(IStorageContext storageContext)
        {
            this.storageContext = storageContext as StorageContext;
            this.bookSet = this.storageContext.Set<Book>();
        }

        public async Task<IEnumerable<Book>> GetAll(int page, int rows)
        {
            return await bookSet.Skip(page * rows).Skip(rows).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAll ()
        {
            return await bookSet.ToListAsync();
        }
 
     

        public async Task<Book> Get(long id)
        {
          return  await bookSet.FirstOrDefaultAsync(book => book.Id == id);
        }

        public async Task Create(Book item)
        {
            await bookSet.AddAsync(item);
            await storageContext.SaveChangesAsync();
        }

        public async Task Update(Book item)
        {
            storageContext.Entry(item).State = EntityState.Modified;
            await storageContext.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var book  = await this.Get(id);
            storageContext.Remove(book);
            await storageContext.SaveChangesAsync();
        }
    }
}
