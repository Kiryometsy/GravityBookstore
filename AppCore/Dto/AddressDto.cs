using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Dto;

public class AddressDto
{
    public string streetNumber { get; set; }
    public string streetName { get; set; }
    public string city { get; set; }
    public int countryId { get; set; }
}
