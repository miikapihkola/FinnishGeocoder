using System;
using System.Collections.Generic;
using System.Text;

namespace FinnishGeocoder.Models
{
    public class AverageResult
    {
        // Group name, or "ALL" for the overall average
        public string Group { get; set; } = string.Empty;

        public double AvgLatitude { get; set; }
        public double AvgLongitude { get; set; }

        // Number of records that contributed to this average
        public int Count { get; set; }

        // Which calculation method was used, carried from appsettings for clarity in output
        public string Method { get; set; } = string.Empty;
    }
}
