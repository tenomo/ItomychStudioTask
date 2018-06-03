using System.Threading.Tasks;
using ItomychStudioTask.API.Models;
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

     
        [HttpGet("{page}/{rows}")]
        public async Task<IActionResult> Get(PaginationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _categoryService.GetAll(model.Page,model.Rows));
        }
    }
}
