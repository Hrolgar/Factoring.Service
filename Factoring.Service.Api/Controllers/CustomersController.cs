using Factoring.Service.Application.Common;
using Factoring.Service.Application.Customers.Commands;
using Factoring.Service.Application.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Factoring.Service.Api.Controllers;

[ApiController] 
[Route("api/customers")]
public class CustomersController(IMediator mediator) : ControllerBase
{
    // // GET /api/customers/{id}
    [HttpGet("{id}", Name = "GetCustomerById")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var query = new GetCustomerById(id);
        var result = await mediator.Send(query);
        
        if (result == null) return NotFound();
        
        return Ok(result);
    }
    
    // GET /api/customers
    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var query = new GetAllCustomersQuery();
        var result = await mediator.Send(query);
        
        return Ok(result);
    }
    
    // Post /api/customers/create
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        var createdCustomerId = await mediator.Send(request);
        
        return CreatedAtAction(
            nameof(GetCustomerById),
            new { id = createdCustomerId },
            createdCustomerId
        );
    }
}


