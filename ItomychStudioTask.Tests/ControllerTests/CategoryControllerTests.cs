using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItomychStudioTask.API.Controllers;
using ItomychStudioTask.API.Models;
using ItomychStudioTask.Business.Services.Categories;
using ItomychStudioTask.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ItomychStudioTask.Tests.ControllerTests
{
    public class CategoryControllerTests
    {
        [Fact]
        public async Task TestGetAll()
        {
            int catsCount = 5;

            #region mock

            var categoriesList = new List<Category>();

            for (int i = 0; i < catsCount; i++)
            {
                categoriesList.Add(new Category()
                {
                    Id = i,
                    Title = Guid.NewGuid().ToString()
                });
            }

            var mockcategoryService = new Mock<ICategoryService>();
            mockcategoryService.Setup(repository => repository.GetAll()).ReturnsAsync(categoriesList);

            #endregion

            var categoriesController = new CategoriesController(mockcategoryService.Object);
            var result = await categoriesController.Get();
            Assert.NotNull(result);
            OkObjectResult okObjectResult = result as OkObjectResult;
            Assert.True(okObjectResult.StatusCode.HasValue);
            Assert.Equal(200, okObjectResult.StatusCode.Value);
            Assert.NotNull(okObjectResult);

            List<Category> categories = okObjectResult.Value as List<Category>;
            Assert.Equal(catsCount, categories.Count);

            for (int i = 0; i < categories.Count; i++)
            {
                Assert.Equal(categoriesList[i], categories[i]);
            }
        }

        [Fact]
        public async void TestGetAllWithPagination_without_page_normalisation()
        {
            var catsCount = 20;
            var expectedPages = 5;
            var expectedRows = 4;

            #region mock

            var categoriesList = new List<Category>();

            for (int i = 0; i < catsCount; i++)
            {
                categoriesList.Add(new Category()
                {
                    Id = i,
                    Title = Guid.NewGuid().ToString()
                });
            }
            var mockcategoryService = new Mock<ICategoryService>();
            mockcategoryService.Setup(repository => repository.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int page, int rows) => categoriesList.Skip(page * rows).Take(rows));

            #endregion

            var categoriesController = new CategoriesController(mockcategoryService.Object);
            for (int page = 0; page < expectedPages; page++)
            {


                var result = await categoriesController.Get(new PaginationModel()
                {
                    Page = page + 1,
                    Rows = expectedRows
                });
                Assert.NotNull(result);
                OkObjectResult okObjectResult = result as OkObjectResult;
                Assert.True(okObjectResult.StatusCode.HasValue);
                Assert.Equal(200, okObjectResult.StatusCode.Value);
                Assert.NotNull(okObjectResult);
                var categories = okObjectResult.Value as IEnumerable<Category>;
                Assert.Equal(expectedRows, categories.Count());
                var expectedCats = categoriesList.Skip(page * expectedRows).Take(expectedRows);
                Assert.Equal(expectedCats, categories);
            }
        }

    }
}
