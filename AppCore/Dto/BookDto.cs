using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Models;

namespace AppCore.Dto;

public class BookDto
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Isbn13 { get; set; }
    public int? LanguageId { get; set; }
    public int? NumPages { get; set; }
    public DateOnly? PublicationDate { get; set; }
    public int? PublisherId { get; set; }
    public publisher publisher { get; set; }
}
