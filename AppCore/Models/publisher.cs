﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

public partial class publisher
{
    [Key]
    public int publisher_id { get; set; }

    [StringLength(400)]
    public string publisher_name { get; set; }

    [InverseProperty("publisher")]
    public virtual ICollection<book> book { get; set; } = new List<book>();
}