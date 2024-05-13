using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Filters;
using AppCore.IRepositories;
using AppCore.Models;
using Infrastracture.Db;
using Infrastracture.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastracture.Repositoreis;

public class BookRepositories : IBookRepositories
{
    private readonly gravity_booksContext _context;
    private readonly ILogger _log;

    public BookRepositories(gravity_booksContext context, ILogger<BookRepositories> log)
    {
        _context = context;
        _log = log;
    }

    public async Task<List<book>> Get(BookFilter filter)
    {
        try
        {
            IQueryable<book> query = _context.book.AsQueryable();

            //Filters
            if (filter.Id != null)
            {
                query = query.Where(x => x.book_id.Equals(filter.Id));
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
            _log.LogError(ex, $"Error get user in Postgres : Message - {ex.Message}");
            return new List<book>();
        }
    }

    public async Task<int> CreateBook(book book)
    {
        try
        {
            var temp=await _context.book.AddAsync(book);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return book.book_id;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"Error creating user in Postgres : Message - {ex.Message}");
            return 0;
        }
    }

    public async Task<bool> UpdateBook(book book, int book_id)
    {
        try
        {
            book? existingBook = await _context.book.FindAsync(book_id);
            var properties = typeof(book).GetProperties();
            foreach (var propery in properties)
            {
                if (propery.PropertyType == typeof(string))
                {
                    string newValue = (string)propery.GetValue(book);
                    if (!string.IsNullOrEmpty(newValue))
                    {
                        propery.SetValue(existingBook, newValue);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"Error updating user in Postgres : Message - {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteBook(int book_id)
    {
        try
        {
            book? existingBook = await _context.book.FindAsync(book_id);
            if (existingBook is null)
            {
                return false;
            }
            _context.book.Remove(existingBook);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"Error deleting user in Postgres : Message - {ex.Message}");
            return false;
        }
    }
    public async Task<int> Count()
    {
        return await _context.book.CountAsync();
    }
}
