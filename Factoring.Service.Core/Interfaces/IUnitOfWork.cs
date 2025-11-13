namespace Factoring.Service.Core.Interfaces;

public interface IUnitOfWork
{
    ICustomerRepository Customers { get; }
    IInvoiceRepository Invoices { get; }
    
    Task<int> CompleteAsync();
}