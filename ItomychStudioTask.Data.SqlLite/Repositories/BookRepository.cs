using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Abstractions;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ItomychStudioTask.Data.SqlLite.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly StorageContext storageContext;
        private readonly DbSet<Book> bookSet;

        public BookRepository(IStorageContext storageContext)
        {
            this.storageContext = storageContext as StorageContext;
            this.bookSet = this.storageContext.Set<Book>();
        }

        public async Task<IEnumerable<Book>> GetAll(int page, int rows)
        {
            return await bookSet.AsNoTracking().Skip(page * rows).Take(rows).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await bookSet.AsNoTracking().ToListAsync();
        }

        public async Task<Book> Get(long id)
        {
            return await bookSet.FirstOrDefaultAsync(book => book.Id == id);
        }

        public async Task Create(Book item)
        {
            await bookSet.AddAsync(item);
            await storageContext.SaveChangesAsync();
        }

        public async Task Update(Book item)
        {
            storageContext.Update(item);
            await storageContext.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var book = await this.Get(id);
            storageContext.Remove(book);
            await storageContext.SaveChangesAsync();
        }
    }
}
