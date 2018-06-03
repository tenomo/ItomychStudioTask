 
using ItomychStudioTask.Data.Abstractions;
using ItomychStudioTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ItomychStudioTask.Data.SqlLite
{
   public class StorageContext : DbContext, IStorageContext
    {
         private string connectionString;

        public StorageContext(string connectionString = "Data Source=ItomychStudioTaskDB.db")
        {
            this.connectionString = connectionString;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(this.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>(etb =>
                {
                    etb.HasKey(e => e.Id);
                    etb.Property(e => e.Id); 
                }
            );
        }
    }
}
    