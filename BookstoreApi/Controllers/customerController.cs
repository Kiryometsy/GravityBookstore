using AppCore.Dto;
using AppCore.Filters;
using BookstoreApi.Controllers.IControllers;
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
        public Task<ActionResult<List<CustomerDto>>> Get([FromQuery] CustomerFilter filter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public Task<ActionResult<int>> Post([FromBody] CustomerDto createCustomer)
        {
            throw new NotImplementedException();
        }
        [HttpPut("{id}")]
        public Task<ActionResult<bool>> Put([FromBody] CustomerDto updateCustomer, [FromQuery] int customer_id)
        {
            throw new NotImplementedException();
        }
    }
}
