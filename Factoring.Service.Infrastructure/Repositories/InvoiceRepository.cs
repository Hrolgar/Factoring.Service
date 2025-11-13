using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Models;
using Factoring.Service.Infrastructure.Data;

namespace Factoring.Service.Infrastructure.Repositories;

public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}