using System;
using System.Collections.Generic;
using System.Text;

namespace FinnishGeocoder.Models
{
    public class AddressRecord
    {
        public string StreetAddress { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
    }
}
