using AppCore.Dto;
using AppCore.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers.IControllers;

public interface ICustomerController
{
    Task<ActionResult<List<CustomerDto>>> Get([FromQuery] CustomerFilter filter);
    Task<ActionResult<int>> Post([FromBody] CustomerDto createCustomer);
    Task<ActionResult<bool>> Put([FromBody] CustomerDto updateCustomer, [FromQuery] int customer_id);
    Task<ActionResult<bool>> Delete([FromQuery] int customer_id);
}
