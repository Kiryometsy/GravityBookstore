﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Filters;

public class CustomerFilter
{
    public int? Id { get; set; }
    //public string publisher_Name { get; set; } = string.Empty;
    public int page { get; set; } = 1;
    public int pageSize { get; set; } = 10;
    public string sortBy { get; set; } = string.Empty;
    public int? sortDirection { get; set; }
}
