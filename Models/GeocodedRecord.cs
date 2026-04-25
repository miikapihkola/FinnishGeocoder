using System;
using System.Collections.Generic;
using System.Text;

namespace FinnishGeocoder.Models
{
    public class GeocodedRecord
    {
        public string StreetAddress { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;

        // The actual query string sent to the API, useful for debugging in review CSV
        public string UsedQuery { get; set; } = string.Empty;

        // Which mode was actually used (may differ from settings if fallback kicked in)
        public string UsedMode { get; set; } = string.Empty;

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // What the API matched, useful for manual review
        public string MatchedLabel { get; set; } = string.Empty;

        // 1.0 = perfect match, null if not returned or not applicable
        public double? Confidence { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
