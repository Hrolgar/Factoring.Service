using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Models;
using Factoring.Service.Infrastructure.Data;

namespace Factoring.Service.Infrastructure.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}