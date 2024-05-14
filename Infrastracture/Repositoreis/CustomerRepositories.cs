using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Filters;
using AppCore.IRepositories;
using AppCore.Models;
using Infrastracture.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastracture.Repositoreis;

public class CustomerRepositories : ICustomerRepositories
{
    private readonly gravity_booksContext _context;
    private readonly ILogger _log;

    public CustomerRepositories(gravity_booksContext context, ILogger<CustomerRepositories> log)
    {
        _context = context;
        _log = log;
    }
    public async Task<List<customer>> Get(CustomerFilter filter)
    {
        throw new NotImplementedException();
    }
    public async Task<int> CreateCustomer(customer customer)
    {
        try
        {
            var temp = await _context.customer.AddAsync(customer);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return customer.customer_id;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"Error creating customer in Postgres : Message - {ex.Message}");
            return 0;
        }
    }
    public async Task<bool> UpdateCustomer(customer customer, int customer_id)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteCustomer(int customer_id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Count()
    {
        return await _context.customer.CountAsync();
    }
}
