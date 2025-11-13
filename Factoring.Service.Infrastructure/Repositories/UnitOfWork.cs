using Factoring.Service.Core.Interfaces;
using Factoring.Service.Infrastructure.Data;

namespace Factoring.Service.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public ICustomerRepository Customers { get; }
    public IInvoiceRepository Invoices { get; }
    
    public UnitOfWork(ApplicationDbContext dbContext
        // ICustomerRepository customerRepository,
        // IInvoiceRepository invoiceRepository
        )
    {
        _dbContext = dbContext;
        Customers = new CustomerRepository(_dbContext);
        Invoices = new InvoiceRepository(_dbContext);
    }
    
    
    public async Task<int> CompleteAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}