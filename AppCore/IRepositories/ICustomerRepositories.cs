using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Filters;
using AppCore.Models;

namespace AppCore.IRepositories;

public interface ICustomerRepositories
{
    Task<List<customer>> Get(CustomerFilter filter);
    Task<int> CreateCustomer(customer customer, address address);
    Task<bool> UpdateCustomer(customer customer, int customer_id);
    Task<bool> DeleteCustomer(int customer_id);
    Task<int> Count();
}
