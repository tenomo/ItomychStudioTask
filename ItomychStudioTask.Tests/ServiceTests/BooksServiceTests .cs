using System;
using System.Collections.Generic;
using System.Linq;
using ItomychStudioTask.Business.Services.Books;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.Models;
using Moq;
using Xunit;

namespace ItomychStudioTask.Tests.ServiceTests
{
    public class BookServiceTests
    {
        [Fact]
        public void TestsGet()
        {
            #region mock

            var booksList = new List<Book>();
            int catsCount = 5;
            for (int i = 0; i < catsCount; i++)
            {
                booksList.Add(new Book()
                {
                    Id = i,
                    Title = Guid.NewGuid().ToString()
                });
            }
            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repository => repository.Get(It.IsAny<long>())).ReturnsAsync((long id) =>
                booksList.FirstOrDefault(book => book.Id == id));
            var mockStorage = new Mock<IStorage>();
            mockStorage.Setup(storage => storage.BookRepository)
                .Returns(mockBookRepository.Object);
            var bookService = new BookService(mockStorage.Object, null);

            #endregion

            for (int i = 0; i < catsCount; i++)
            {
                Assert.NotNull(bookService.Get(i));
            }
            
        }

        [Fact]
        public void TestGetAll()
        {
            #region mock

            var booksList = new List<Book>();
            int catsCount = 5;
            for (int i = 0; i < catsCount; i++)
            {
                booksList.Add(new Book()
                {
                    Id = i,
                    Title = Guid.NewGuid().ToString()
                });
            }
            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repository => repository.GetAll()).ReturnsAsync(booksList);
            var mockStorage =
                new Mock<IStorage>();
            mockStorage.Setup(storage => storage.BookRepository)
                .Returns(mockBookRepository.Object);
            var bookService = new BookService(mockStorage.Object, null);

            #endregion

            var cats = bookService.GetAll().Result;
            Assert.Equal(catsCount, cats.Count());
        }

        [Fact]
        public void TestGetAllWithPagination_without_page_normalisation()
        {
            var catsCount = 20;

            #region mock

            var booksList = new List<Book>();

            for (int i = 0; i < catsCount; i++)
            {
                booksList.Add(new Book()
                {
                    Id = i,
                    Title = Guid.NewGuid().ToString()
                });
            }
            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repository => repository.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int page, int rows) => booksList.Skip(page * rows).Take(rows));
            var mockStorage =
                new Mock<IStorage>();
            mockStorage.Setup(storage => storage.BookRepository)
                .Returns(mockBookRepository.Object);
            var bookService = new BookService(mockStorage.Object, null);

            #endregion

            var expectedPages = 5;
            var expectedRows = 4;
            for (int i = 0; i < expectedPages; i++)
            {
                var cats = bookService.GetAll(i, expectedRows).Result;
                Assert.Equal(expectedRows, cats.Count());
            }
        }

        [Fact]
        public void TestCreateBook()
        {
            #region mock

            var booksList = new List<Book>();
            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repository => repository.GetAll()).ReturnsAsync(booksList);
            mockBookRepository.Setup(repository => repository.Create(It.IsAny<Book>()))
                .Callback((Book book) => booksList.Add(book));
            var mockStorage =
                new Mock<IStorage>();
            mockStorage.Setup(storage => storage.BookRepository)
                .Returns(mockBookRepository.Object);
            var bookService = new BookService(mockStorage.Object, null);

            #endregion

            Assert.Empty(bookService.GetAll().Result);
            bookService.Create(new Book());
            Assert.Single(bookService.GetAll().Result);
        }

        [Fact]
        public void TestDeleteBook()
        {
            #region mock

            var booksList = new List<Book>();
            var targetBook = new Book();
            booksList.Add(targetBook);
            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repository => repository.GetAll()).ReturnsAsync(booksList);
            mockBookRepository.Setup(repository => repository.Delete(It.IsAny<long>()))
                .Callback((long bookId) => booksList.RemoveAt((int) bookId));
            var mockStorage =
                new Mock<IStorage>();
            mockStorage.Setup(storage => storage.BookRepository)
                .Returns(mockBookRepository.Object);
            var bookService = new BookService(mockStorage.Object, null);

            #endregion

            Assert.Single(bookService.GetAll().Result);
            bookService.Delete(targetBook.Id);
            Assert.Empty(bookService.GetAll().Result);
        }
    }
}
