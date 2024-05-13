﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

public partial class address
{
    [Key]
    public int address_id { get; set; }

    [StringLength(10)]
    public string street_number { get; set; }

    [StringLength(200)]
    public string street_name { get; set; }

    [StringLength(100)]
    public string city { get; set; }

    public int? country_id { get; set; }

    [ForeignKey("country_id")]
    [InverseProperty("address")]
    public virtual country country { get; set; }

    [InverseProperty("dest_address")]
    public virtual ICollection<cust_order> cust_order { get; set; } = new List<cust_order>();

    [InverseProperty("address")]
    public virtual ICollection<customer_address> customer_address { get; set; } = new List<customer_address>();
}