using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Dto;
using AppCore.Filters;
using AppCore.IRepositories;
using AppCore.Models;
using Infrastracture.Db;
using Infrastracture.Helper;
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
        try
        {
            IQueryable<customer> query = _context.customer
                .Include(x => x.customer_address)
                .ThenInclude(ca => ca.address)
                .AsQueryable();

            //Filters
            if (filter.Id != null)
            {
                query = query.Where(x => x.customer_id.Equals(filter.Id));
            }

            //Sort
            if (!string.IsNullOrEmpty(filter.sortBy))
            {
                query = SortHelper.ApplyDynamicSorting(query, filter.sortBy, filter.sortDirection);
            }

            //Pagination
            query = query.Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize);

            var result = await query.ToListAsync().ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"Error get customer in Postgres : Message - {ex.Message}");
            return new List<customer>();
        }
    }
    public async Task<int> CreateCustomer(customer customer, address address)
    {
        try
        {
            var temp = await _context.customer.AddAsync(customer);
            await _context.address.AddAsync(address);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            customer_address adres = new customer_address()
            {
                customer_id = customer.customer_id,
                address_id = address.address_id,
                status_id = 1
            };

            await _context.customer_address.AddAsync(adres);
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
