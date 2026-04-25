# FinnishGeocoder
Version: 0.0

This is console app that:

1. Part
Takes csv of addresses and converts them into coordinates using digitransit api and exports them as csv for manual review

2. Part
App that takes csv of coordinates and then calculates average location for all and specified group.
Only Rows with OK tag are used for calculation

## Configuring appsettings.json

example data row
Streetname 10 A 11;00100;Helsinki;Group A & Group B

"GeocodingMode" // Query sent to API
StreetAddress = "Streetname 10, 00100 HELSINKI"
PostalCode = "00100"
City = "Helsinki"

"MissingFieldFallback" // Behaviour
Exclude = Row is skipped entirely, logged as EXCLUDED_MISSING_FIELD
Fallback = Try next available field in priority order: StreetAddress → PostalCode → City

"CoordinateBounds": // Settings for reducing too far away coordinates, default values correspond to Finland
{
    "Enabled": true, // true or false, tells if setting is used
    "MinLat": 59.5,
    "MaxLat": 70.5,
    "MinLon": 19.0,
    "MaxLon": 32.0
},

"AverageMethod" // Method
Flat = Arithmetic mean of lat/lon directly
Spherical = Convert each point to 3D cartesian (x,y,z) → average → convert back to lat/lon

"CsvConfig"{ // Make sure values in here match your own csv files
    "GroupSeparator" = "&" // This is separator for Group field, will ignore spaces after or before separator "a&b", "a & b", "a    &b" all will work similarly
}

## How to use

1. Change your appsettings.json and change values that are needed

### Geocoded csv info
MatchedLabel // What the API matched, useful for manual review
Confidence // 1.0 for perfect match, null if not returned or not applicable


## License & Attribution

### This Project
This project is licensed under the [MIT License](LICENSE).

### Geocoding Data — Digitransit API
This application uses the [Digitransit Geocoding API](https://digitransit.fi/en/developers/apis/3-geocoding-api/)
provided by Helsinki Region Transportation (HSL), Fintraffic, and Waltti Solutions Oy.

Digitransit data is licensed under **Creative Commons Attribution 4.0 International (CC BY 4.0)**.

As required by the CC BY license:
> © Digitransit — data retrieved at time of geocoding

The geocoded coordinate data produced by this application is derived from Digitransit data.
If you share or publish that output data, you must retain the above attribution.

#### Note on OpenStreetMap data
Part of the geocoding results may be sourced from OpenStreetMap data via Digitransit.
OpenStreetMap-based geographical and address data is governed by the
**[Open Database License (ODbL)](https://opendatacommons.org/licenses/odbl/)**.
This applies in practice to parts of the geocoding results.

#### Digitransit API Usage
Use of the Digitransit API requires registration at [portal-api.digitransit.fi](https://portal-api.digitransit.fi/)
and acceptance of the [Digitransit API Terms of Use](https://digitransit.fi/en/developers/apis/).
This application does not include or distribute any API keys.
Each user is responsible for obtaining their own subscription key and complying with the terms.
