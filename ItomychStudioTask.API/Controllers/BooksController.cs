using System.Threading.Tasks;
using AutoMapper;
using ItomychStudioTask.API.Attributes;
using ItomychStudioTask.API.Models;
using ItomychStudioTask.Business.Services.Books;
using ItomychStudioTask.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItomychStudioTask.API.Controllers
{
    /// <summary>
    /// Books endpoint provides CRUD operations with books.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Books")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper  _mapper;
        private readonly IBookValidationService _bookValidationService;

        public BooksController(IBookService bookService, IMapper mapper, IBookValidationService bookValidationService)
        {
            _bookService = bookService;
            _mapper = mapper;
            this._bookValidationService = bookValidationService;
        }

       
        /// <summary>
        /// Returns books collection.
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        // GET: api/Books
        public async Task<IActionResult> Get()
        {
            return Ok( await _bookService.GetAll());
        }


        /// <summary>
        /// Returns books collection with pagination.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        // GET: api/Books/1/10
        [HttpGet("{page}/{rows}")]
        public async Task<IActionResult> Get(PaginationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _bookService.GetAll(model.Page,model.Rows));
        }


        /// <summary>
        /// Returns a book by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: api/Books/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _bookService.Get(id));
        }

        /// <summary>
        /// Creates a new book. A book must has unique and belongs to some category. 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        
        // POST: api/Books
        [HttpPost]
       [ValidateModel]
        
       public async Task<IActionResult> Post(  BookCreateModel book)
        { 

            var bookEntity = _mapper.Map<Book>(book);
            if (!_bookValidationService.IsBookBelongsToCategory(bookEntity))
                return BadRequest($"Can not save book. Category by id {book.CategoryId} was not found.");
            await _bookService.Create(bookEntity);
            return Ok();
        }
        /// <summary>
        /// Updates a book. A book must and belongs to some category. 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
       
        // PUT: api/Books/5
        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> Put( BookEditModel book)
        {
            var bookEntity = _mapper.Map<Book>(book);

            if (!_bookValidationService.IsBookExists(bookEntity))
                return NotFound("The book by id {book.Id} was not found.");

            if (!_bookValidationService.IsBookBelongsToCategory(bookEntity))
                return BadRequest($"Can not save. Category by id {book.Id} was not found.");

            await _bookService.Update(bookEntity);
            return Ok();
        }
        /// <summary>
        /// Deletes book by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_bookValidationService.IsBookExists(id))
                return NotFound("The book by id {book.Id} was not found.");
            await _bookService.Delete(id);
            return Ok();
        }
    }
}
