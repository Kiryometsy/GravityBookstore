using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Dto;
using AppCore.Filters;

namespace Infrastracture.Service.IService;

public interface ICustomerService
{
    Task<List<CustomerDto>> Get(CustomerFilter filter);
    Task<int> Post(CustomerDto customerDto);
    Task<bool> Put(CustomerDto customerDto, int customer_id);
    Task<bool> Delete(int customer_id);
}
