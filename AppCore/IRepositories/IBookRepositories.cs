using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Filters;
using AppCore.Models;

namespace AppCore.IRepositories;

public interface IBookRepositories
{
    Task<List<book>> Get(BookFilter filter);
    Task<int> CreateBook(book book);
    Task<bool> UpdateBook(book book, int book_id);
    Task<bool> DeleteBook(int book_id);
    Task<int> Count();
}
