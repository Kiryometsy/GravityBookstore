using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AppCore.Dto;
using AppCore.Filters;
using AppCore.IRepositories;
using AppCore.Models;
using AutoMapper;
using Infrastracture.Service.IService;

namespace Infrastracture.Service;

public class BookService : IBookService
{
    private readonly IBookRepositories _bookRepositories;
    private readonly IMapper _mapper;

    public BookService(IBookRepositories bookRepositories, IMapper mapper)
    {
        _bookRepositories = bookRepositories;
        _mapper = mapper;
    }

    public async Task<List<BookDto>> Get(BookFilter filter)
    {
        List<book> getBookList = await _bookRepositories.Get(filter);
        List<BookDto> mappedBook = _mapper.Map<List<BookDto>>(getBookList);
        return mappedBook;
    }

    public async Task<int> Post(BookDto bookDto)
    {
        bookDto.BookId = _bookRepositories.Count().Result+1;
        book mappedBook = _mapper.Map<book>(bookDto);
        int createBookDto = await _bookRepositories.CreateBook(mappedBook);
        return createBookDto;
    }

    public async Task<bool> Put(BookDto bookDto, int book_id)
    {
        book mappedBook = _mapper.Map<book>(bookDto);
        bool changeBook = await _bookRepositories.UpdateBook(mappedBook, book_id);
        return changeBook;
    }
    public async Task<bool> Delete(int book_id)
    {
        bool deleteResult = await _bookRepositories.DeleteBook(book_id);
        return deleteResult;
    }

    

    

    
}
