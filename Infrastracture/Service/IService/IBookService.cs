using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Dto;
using AppCore.Filters;

namespace Infrastracture.Service.IService;

public interface IBookService
{
    Task<List<BookDto>> Get(BookFilter filter);
    Task<int> Post(BookDto bookDto);
    Task<bool> Put(BookDto bookDto, int book_id);
    Task<bool> Delete(int book_id);
}
