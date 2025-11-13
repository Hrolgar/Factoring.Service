using Factoring.Service.Application.Dtos;
using Factoring.Service.Core.Models;

namespace Factoring.Service.Application.Mappings;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Invoice, InvoiceDto>();
        CreateMap<Customer, CustomerDto>();

    }
    
}