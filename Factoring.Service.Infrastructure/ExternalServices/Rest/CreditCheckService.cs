using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Interfaces.IExternalServices;

namespace Factoring.Service.Infrastructure.ExternalServices.Rest;

public class CreditCheckService(IUnitOfWork unitOfWork) : ICreditCheckService
{
    public async Task<bool> IsCustomerCreditWorthyAsync(Guid customerId)
    {
        var customer = await unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null) return false;
        
        return !(customer.CreditScore < 550);
    }
}
