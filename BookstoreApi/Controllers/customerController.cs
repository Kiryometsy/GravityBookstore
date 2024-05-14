using AppCore.Dto;
using AppCore.Filters;
using BookstoreApi.Controllers.IControllers;
using Infrastracture.Service;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customerController : ControllerBase, ICustomerController
    {
        private readonly ICustomerService _customerService;

        public customerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpDelete]
        public Task<ActionResult<bool>> Delete([FromQuery] int customer_id)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> Get([FromQuery] CustomerFilter filter)
        {
            List<CustomerDto> result = await _customerService.Get(filter);
            if (result.Count <= 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CustomerDto createCustomer)
        {
            int createCustomerId = await _customerService.Post(createCustomer);
            if (createCustomerId == 0)
            {
                return BadRequest("unable to create customer.");
            }
            return CreatedAtAction(nameof(Post), new { customerId = createCustomerId });
        }
        [HttpPut("{id}")]
        public Task<ActionResult<bool>> Put([FromBody] CustomerDto updateCustomer, [FromQuery] int customer_id)
        {
            throw new NotImplementedException();
        }
    }
}
