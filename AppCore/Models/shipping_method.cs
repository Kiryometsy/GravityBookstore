﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

public partial class shipping_method
{
    [Key]
    public int method_id { get; set; }

    [StringLength(100)]
    public string method_name { get; set; }

    [Precision(6, 2)]
    public decimal? cost { get; set; }

    [InverseProperty("shipping_method")]
    public virtual ICollection<cust_order> cust_order { get; set; } = new List<cust_order>();
}