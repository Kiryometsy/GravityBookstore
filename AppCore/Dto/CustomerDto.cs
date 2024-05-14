using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Models;

namespace AppCore.Dto;

public class CustomerDto
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email {  get; set; }
    public AddressDto address { get; set; }
}
