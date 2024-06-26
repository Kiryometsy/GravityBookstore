﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

public partial class order_history
{
    [Key]
    public int history_id { get; set; }

    public int? order_id { get; set; }

    public int? status_id { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? status_date { get; set; }

    [ForeignKey("order_id")]
    [InverseProperty("order_history")]
    public virtual cust_order order { get; set; }

    [ForeignKey("status_id")]
    [InverseProperty("order_history")]
    public virtual order_status status { get; set; }
}