using System;
using System.Threading.Tasks;
using ItomychStudioTask.API.Attributes;
using ItomychStudioTask.API.Models;
using ItomychStudioTask.Business.Services.Books;
using ItomychStudioTask.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItomychStudioTask.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Books")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok( await _bookService.GetAll());
        }  
        
        // GET: api/Books/1/10
         
        [HttpGet("{page}/{rows}")]
        public async Task<IActionResult> Get(PaginationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _bookService.GetAll(model.Page,model.Rows));
        }

        // GET: api/Books/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _bookService.Get(id));
        }
        
        // POST: api/Books
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Post([FromBody] Book book)
        //public async Task<IActionResult> Post([FromBody] BookCreateModel book)
        { 
            if (!_bookService.BookValidationService.IsBookBelongsToCategory(book))
                return BadRequest($"Can not save book. Category by id {book.CategoryId} was not found.");

            await _bookService.Create(book);
            return Ok();
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Put([FromBody]BookEditModel book)
        {

            if (!_bookService.BookValidationService.IsBookExists(book))
                return NotFound("The book by id {book.Id} was not found.");

            if (!_bookService.BookValidationService.IsBookBelongsToCategory(book))
                return BadRequest($"Can not save. Category by id {book.Id} was not found.");

            await _bookService.Update(book);
            return Ok();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_bookService.BookValidationService.IsBookExists(id))
                return NotFound("The book by id {book.Id} was not found.");
            await _bookService.Delete(id);
            return Ok();
        }
    }
}
