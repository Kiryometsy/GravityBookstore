using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

[PrimaryKey("book_id", "author_id")]
public class book_author
{
    [Key]
    [ForeignKey("book")]
    public int book_id {  get; set; }
    public book book { get; set; }
    [Key]
    [ForeignKey("author")]
    public int author_id { get; set; }

    public author author { get; set; }
}
