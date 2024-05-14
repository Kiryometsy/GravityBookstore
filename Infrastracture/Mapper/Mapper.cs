using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Dto;
using AppCore.Models;
using AutoMapper;

namespace Infrastracture.Mapper;

public class Mapper : Profile
{
    public Mapper()
    {
        //book
        CreateMap<book, BookDto>()
            .AfterMap((src, dest) =>
            {
                dest.BookId = src.book_id;
                dest.Title = src.title;
                dest.Isbn13 = src.isbn13;
                dest.LanguageId = src.language_id;
                dest.NumPages = src.num_pages;
                dest.PublicationDate = src.publication_date;
                dest.PublisherId = src.publisher_id;
            });
        CreateMap<BookDto, book>()
            .AfterMap((src, dest) =>
            {
                dest.book_id = src.BookId;
                dest.title = src.Title;
                dest.isbn13 = src.Isbn13;
                dest.language_id = src.LanguageId;
                dest.num_pages = src.NumPages;
                dest.publication_date = src.PublicationDate;
                dest.publisher_id = src.PublisherId;
            });
        //customer
        CreateMap<customer, CustomerDto>()
            .AfterMap((src, dest) =>
            {
                dest.firstName = src.first_name;
                dest.lastName = src.last_name;
                dest.email = src.email;
            });
        CreateMap<CustomerDto, customer>()
            .AfterMap((src, dest) =>
            {
                dest.first_name = src.firstName;
                dest.last_name = src.lastName;
                dest.email = src.email;
            });
    }
}
