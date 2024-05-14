using AppCore.Dto;
using AppCore.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers.IControllers;

public interface IBookController
{
    Task<ActionResult<List<BookDto>>> Get([FromQuery] BookFilter filter);
    Task<ActionResult<int>> Post([FromBody] BookDto createBook);
    Task<ActionResult<bool>> Put([FromBody] BookDto updateBook, [FromQuery] int book_id);
    Task<ActionResult<bool>> Delete([FromQuery] int book_id);
}
