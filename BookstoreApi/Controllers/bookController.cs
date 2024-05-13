using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppCore.Models;
using Infrastracture.Db;
using Infrastracture.Service.IService;
using AppCore.Dto;
using AppCore.Filters;

namespace BookstoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public bookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/book
        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> Get([FromQuery] BookFilter filter)
        {
            List<BookDto> result = await _bookService.Get(filter);
            if (result.Count <= 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/book
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] BookDto createBook)
        {
            int createBookId = await _bookService.Post(createBook);
            if (createBookId == 0)
            {
                return BadRequest("unable to create book.");
            }
            return CreatedAtAction(nameof(Post), new {id = createBookId});
        }

        // PUT: api/book/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put([FromBody] BookDto updateBook, [FromQuery] int book_id)
        {

            bool updateResult = await _bookService.Put(updateBook, book_id);
            if (!updateResult)
            {
                return BadRequest();
            }
            return Ok(updateResult);
        }

        // DELETE: api/books/5
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromQuery] int book_id)
        {
            bool deleteResult = await _bookService.Delete(book_id);
            if (!deleteResult)
            {
                return BadRequest();
            }
            return Ok(deleteResult);
        }
    }
}
