using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ItomychStudioTask.API.Models;
using ItomychStudioTask.API.Utils;
using ItomychStudioTask.Business.Services.Categories;
using Microsoft.AspNetCore.Mvc;

namespace ItomychStudioTask.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Returns category collection.
        /// </summary>
        /// <returns></returns>

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryService.GetAll());
        }

        /// <summary>
        /// Returns category collection with pagination.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        // GET: api/Categories/1/1
        [HttpGet("{page}/{rows}")]

        public async Task<IActionResult> Get(int page, int rows) { 
            if (!PaginationUtil.ValidatePagination(page, rows))
                return BadRequest("Page and rows should be greater than 0");

            page = PaginationUtil.GetNormalizedPage(page);
            
            return Ok(await _categoryService.GetAll(page, rows));
        }
    }
}
