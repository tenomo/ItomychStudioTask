using System;
using System.Collections.Generic;
using System.Linq;
using ItomychStudioTask.Business.Services.Categories;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.Models;
using Moq;
using Xunit;

namespace ItomychStudioTask.Tests.ServiceTests
{
    public class CetegoryServiceTests
    {
        [Fact]
        public void TestGetAll()
        {
            var categoriesList = new List<Category>();
            int catsCount = 5;
            for (int i = 0; i < catsCount; i++)
            {
                categoriesList.Add(new Category()
                {
                    Id = i,
                    Title = Guid.NewGuid().ToString()
                });
            }
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repository => repository.GetAll()).ReturnsAsync(categoriesList);

            var mockStorage =
                new Mock<IStorage>();
            mockStorage.Setup(storage => storage.CategoryRepository)
                .Returns(mockCategoryRepository.Object);

            var categoryService = new CategoryService(mockStorage.Object);

            var cats = categoryService.GetAll().Result;

            Assert.Equal(catsCount, cats.Count());
        }

        [Fact]
        public void TestGetAllWithPagination_without_page_normalisation()
        {
            var categoriesList = new List<Category>();
            var catsCount = 20;
            var expectedPages = 5;
            var expectedRows = 4;
            for (int i = 0; i < catsCount; i++)
            {
                categoriesList.Add(new Category()
                {
                    Id = i,
                    Title = Guid.NewGuid().ToString()
                });
            }
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repository => repository.GetAll(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((int page, int rows) => categoriesList.Skip(page * rows).Take(rows));

            var mockStorage =
                new Mock<IStorage>();
            mockStorage.Setup(storage => storage.CategoryRepository)
                    .Returns(mockCategoryRepository.Object);

            var categoryService = new CategoryService(mockStorage.Object);

            for (int i = 0; i < expectedPages; i++)
            {
                var cats = categoryService.GetAll(i, expectedRows).Result;
                Assert.Equal(expectedRows, cats.Count());
            }
        }
    }
}
