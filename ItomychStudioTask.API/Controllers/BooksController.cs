using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper  _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
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
         
       public async Task<IActionResult> Post(  BookCreateModel book)
        { 

            var bookEntity = _mapper.Map<Book>(book);
            if (!_bookService.BookValidationService.IsBookBelongsToCategory(bookEntity))
                return BadRequest($"Can not save book. Category by id {book.CategoryId} was not found.");
            await _bookService.Create(bookEntity);
            return Ok();
        }

        // PUT: api/Books/5
        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> Put( BookEditModel book)
        {
            var bookEntity = _mapper.Map<Book>(book);

            if (!_bookService.BookValidationService.IsBookExists(bookEntity))
                return NotFound("The book by id {book.Id} was not found.");

            if (!_bookService.BookValidationService.IsBookBelongsToCategory(bookEntity))
                return BadRequest($"Can not save. Category by id {book.Id} was not found.");

            await _bookService.Update(bookEntity);
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
