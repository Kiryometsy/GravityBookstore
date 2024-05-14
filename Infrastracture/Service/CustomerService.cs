using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Dto;
using AppCore.Filters;
using AppCore.IRepositories;
using AppCore.Models;
using AutoMapper;
using Infrastracture.Repositoreis;
using Infrastracture.Service.IService;

namespace Infrastracture.Service;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepositories _customerRepositories;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepositories customerRepositories, IMapper mapper)
    {
        _customerRepositories = customerRepositories;
        _mapper = mapper;
    }
    public async Task<List<CustomerDto>> Get(CustomerFilter filter)
    {
        List<customer> getCustomerList = await _customerRepositories.Get(filter);
        List<CustomerDto> mappedCustomer = _mapper.Map<List<CustomerDto>>(getCustomerList);
        return mappedCustomer;
    }
    public async Task<int> Post(CustomerDto customerDto)
    {
        address adres = _mapper.Map<address>(customerDto.address);
        customer mappedCustomer = _mapper.Map<customer>(customerDto);
        mappedCustomer.customer_id = _customerRepositories.Count().Result + 1;
        adres.address_id = _customerRepositories.CountAddreses().Result + 1;
        int createCustomerDto = await _customerRepositories.CreateCustomer(mappedCustomer, adres);
        return createCustomerDto;
    }
    public async Task<bool> Put(CustomerDto customerDto, int customer_id)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> Delete(int customer_id)
    {
        throw new NotImplementedException();
    }

}
