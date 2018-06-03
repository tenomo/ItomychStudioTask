using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ItomychStudioTask.API.Controllers;
using ItomychStudioTask.API.Models;
using ItomychStudioTask.API.Profiles;
using ItomychStudioTask.Business.Services.Books;
using ItomychStudioTask.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ItomychStudioTask.Tests.ControllerTests
{
    public class BooksControllerTests
    {
        [Fact]
        public async Task TestGetAll()
        {
            #region mock

            var booksList = new List<Book>();
            int booksCount = 5;
            for (int i = 0; i < booksCount; i++)
            {
                booksList.Add(new Book()
                {
                    Id = i,
                    Title = Guid.NewGuid().ToString()
                });
            }

            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(repository => repository.GetAll()).ReturnsAsync(booksList);

            #endregion

            var booksController = new BooksController(mockBookService.Object, null, null);
            var result = await booksController.Get();
            Assert.NotNull(result);
            OkObjectResult okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.True(okObjectResult.StatusCode.HasValue);
            Assert.Equal(200, okObjectResult.StatusCode.Value);
            List<Book> books = okObjectResult.Value as List<Book>;
            Assert.Equal(booksCount, books.Count);

            for (int i = 0; i < books.Count; i++)
            {
                Assert.Equal(booksList[i], books[i]);
            }
        }

        [Fact]
        public async void TestGetAllWithPagination_without_page_normalisation()
        {
            var booksList = new List<Book>();
            var booksCount = 20;
            var expectedPages = 5;
            var expectedRows = 4;

            #region nock

            for (int i = 0; i < booksCount; i++)
            {
                booksList.Add(new Book()
                {
                    Id = i,
                    Title = Guid.NewGuid().ToString()
                });
            }
            var mockbookService = new Mock<IBookService>();
            mockbookService.Setup(repository => repository.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int page, int rows) => booksList.Skip(page * rows).Take(rows));

            #endregion

            var booksController = new BooksController(mockbookService.Object, null, null);
            for (int page = 0; page < expectedPages; page++)
            {


                var result = await booksController.Get(new PaginationModel()
                {
                    Page = page + 1,
                    Rows = expectedRows
                });


                Assert.NotNull(result);
                OkObjectResult okObjectResult = result as OkObjectResult;
                Assert.NotNull(okObjectResult);
                Assert.True(okObjectResult.StatusCode.HasValue);
                Assert.Equal(200, okObjectResult.StatusCode.Value);
                var books = okObjectResult.Value as IEnumerable<Book>;
                Assert.Equal(expectedRows, books.Count());
                var expectedBooks = booksList.Skip(page * expectedRows).Take(expectedRows);
                Assert.Equal(expectedBooks, books);
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            #region mock

            var booksList = new List<Book>();
            var mockBookService = new Mock<IBookService>();
        
            mockBookService.Setup(repository => repository.Create(It.IsAny<Book>()))
            .Callback((Book book) => booksList.Add(book));

            IMapper maper = new Mapper(new MapperConfiguration(expression =>
                expression.AddProfile(new DomainProfile())));

            var categoriesList = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Title = Guid.NewGuid().ToString()
                }
            };

            var mockBookValidationService = new Mock<IBookValidationService>();
           

            mockBookValidationService.Setup(service => service.IsBookBelongsToCategory(It.IsAny<Book>())).Returns((Book model) =>
            {
                return categoriesList.Any(category => category.Id == model.CategoryId);
            });
            #endregion

            var booksController = new BooksController(mockBookService.Object, maper, mockBookValidationService.Object);

           
            Assert.Empty(booksList);
            var result = await booksController.Post(new BookCreateModel()
            {
                CategoryId = 0,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            });

            Assert.NotNull(result);


            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestObjectResult);


              result = await booksController.Post(new BookCreateModel()
            {
                CategoryId = 1,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            });
//            var okResult = result as OkResult;
//            Assert.NotNull(okResult);
           
         
            Assert.Single(booksList);
        }
    }
}
