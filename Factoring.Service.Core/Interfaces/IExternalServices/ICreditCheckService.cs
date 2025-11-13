namespace Factoring.Service.Core.Interfaces.IExternalServices;

public interface ICreditCheckService
{
    Task<bool> IsCustomerCreditWorthyAsync(Guid customerId);
}