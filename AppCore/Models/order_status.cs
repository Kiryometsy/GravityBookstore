﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Models;

public partial class order_status
{
    [Key]
    public int status_id { get; set; }

    [StringLength(20)]
    public string status_value { get; set; }

    [InverseProperty("status")]
    public virtual ICollection<order_history> order_history { get; set; } = new List<order_history>();
}