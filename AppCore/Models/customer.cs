﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

public partial class customer
{
    [Key]
    public int customer_id { get; set; }

    [StringLength(200)]
    public string first_name { get; set; }

    [StringLength(200)]
    public string last_name { get; set; }

    [StringLength(350)]
    public string email { get; set; }

    [InverseProperty("customer")]
    public virtual ICollection<cust_order> cust_order { get; set; } = new List<cust_order>();

    [InverseProperty("customer")]
    public virtual ICollection<customer_address> customer_address { get; set; } = new List<customer_address>();
}