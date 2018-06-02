using System.Threading.Tasks;
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
         
        [HttpGet]
        public async Task< IActionResult> Get()
        {
            return Ok(await _categoryService.GetAll());
        }

        [HttpGet]
        [HttpDelete("{page}/{rows}")]
        public async Task<IActionResult> Get(int page, int rows)
        {
            return Ok(await _categoryService.GetAll());
        }
    }
}
