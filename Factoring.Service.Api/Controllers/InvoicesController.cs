using Factoring.Service.Application.Invoices.Commands;
using Factoring.Service.Application.Invoices.Queries;
using Microsoft.AspNetCore.Mvc;
using Factoring.Service.Application.Common;
using Microsoft.AspNetCore.Authorization;

namespace Factoring.Service.Api.Controllers;

[ApiController, Authorize] 
[Route("api/invoices")]
public class InvoicesController(IMediator mediator) : ControllerBase
{
    // GET /api/invoices
    [HttpGet]
    public async Task<IActionResult> GetInvoices()
    {
        var query = new GetAllInvoicesQuery();
        var result = await mediator.SendAsync(query);

        return Ok(result);
    }
    
    // GET /api/invoices/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetInvoicesById(Guid id)
    {
        var query = new GetInvoiceByIdQuery (id);
        var result = await mediator.SendAsync(query);
        
        if (result == null) return NotFound();
        
        return Ok(result);
    }
    
    // GET /api/invoices/{id}/finance
    [HttpPost("{id:guid}/finance")]
    public async Task<IActionResult> FinanceInvoice(Guid id)
    {
        var command = new FinanceInvoiceCommand(id);
        // await _mediator.Send(command);
        await mediator.SendAsync(command);

        Console.WriteLine($"Invoice {id} financed via API.");
        
        return NoContent();
    }
    
    // POST /api/invoices/create
    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody]CreateInvoiceCommand command)
    {
        var createdInvoiceId = await mediator.SendAsync(command);

        return CreatedAtAction(
            nameof(GetInvoicesById),
            new { id = createdInvoiceId },
            createdInvoiceId
        );
    }
    
    // PUT /api/invoices/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateInvoice(Guid id, [FromBody]UpdateInvoiceCommand command)
    {
        command.Id = id;
        if (id != command.Id)
            return BadRequest("ID in URL does not match ID in body.");
        
        var result = await mediator.SendAsync(command);
        if (!result) return NotFound();
        
        return NoContent();
    }
    
    // DELETE /api/invoices/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteInvoice(Guid id)
    {
        var command = new DeleteInvoiceCommand { Id = id };
        var result = await mediator.SendAsync(command);
        if (!result) return NotFound();
        
        return NoContent();
    }
    
    
    // [HttpGet] 
    // public ActionResult<List<Invoice>> GetInvoices() => new List<Invoice>()
    // {
    //     new Invoice
    //     {
    //         Id = Guid.NewGuid(),
    //         Amount = 1000.00m,
    //         IssuedDate = DateTime.UtcNow,
    //         InvoiceNumber = "INV-12345",
    //         CustomerId = Guid.NewGuid(),
    //         DueDate = DateTime.UtcNow.AddDays(30),
    //         CreatedOn = DateTime.UtcNow,
    //         ModifiedOn = DateTime.UtcNow,
    //     },
    //     new Invoice
    //     {
    //         Id = Guid.NewGuid(),
    //         Amount = 2000.00m,
    //         IssuedDate = DateTime.UtcNow,
    //         InvoiceNumber = "INV-67890",
    //         CustomerId = Guid.NewGuid(),
    //         DueDate = DateTime.UtcNow.AddDays(30),
    //         CreatedOn = DateTime.UtcNow,
    //         ModifiedOn = DateTime.UtcNow
    //     },
    //     new Invoice
    //     {
    //         Id = Guid.NewGuid(),
    //         Amount = 3000.00m,
    //         IssuedDate = DateTime.UtcNow,
    //         InvoiceNumber = "INV-123456",
    //         CustomerId = Guid.NewGuid(),
    //         DueDate = DateTime.UtcNow.AddDays(30),
    //         CreatedOn = DateTime.UtcNow,
    //         ModifiedOn = DateTime.UtcNow
    //     }
    // };
    
    
}