{
  "Dates": {
    "OutboundDates": [      {  "PartialDate": "2019-01-20",
        "QuoteIds": [
          1,
          2
        ],
        "Price": 1134.0,
        "QuoteDateTime": "2019-01-10T10:39:00"
      }
    ]
  },
  "Quotes": [
    {
      "QuoteId": 1,
      "MinPrice": 1134.0,
      "Direct": false,
      "OutboundLeg": {
        "CarrierIds": [
          1760
        ],
        "OriginId": 68768,
        "DestinationId": 45198,
        "DepartureDate": "2019-01-20T00:00:00"
      },
      "QuoteDateTime": "2019-01-10T10:39:00"
    },
    {
      "QuoteId": 2,
      "MinPrice": 1457.0,
      "Direct": true,
      "OutboundLeg": {
        "CarrierIds": [
          852
        ],
        "OriginId": 68768,
        "DestinationId": 45198,
        "DepartureDate": "2019-01-20T00:00:00"
      },
      "QuoteDateTime": "2019-01-10T10:39:00"
    }
  ],
  "Places": [
    {
      "PlaceId": 45198,
      "IataCode": "CMN",
      "Name": "Casablanca Mohamed V.",
      "Type": "Station",
      "SkyscannerCode": "CMN",
      "CityName": "Casablanca",
      "CityId": "CASA",
      "CountryName": "Morocco"
    },
    {
      "PlaceId": 68768,
      "IataCode": "MXP",
      "Name": "Milan Malpensa",
      "Type": "Station",
      "SkyscannerCode": "MXP",
      "CityName": "Milan",
      "CityId": "MILA",
      "CountryName": "Italy"
    }
  ],
  "Carriers": [
    {
      "CarrierId": 852,
      "Name": "Royal Air Maroc"
    },
    {
      "CarrierId": 1760,
      "Name": "TAP"
    }
  ],
  "Currencies": [
    {
      "Code": "MAD",
      "Symbol": "د.م.‏",
      "ThousandsSeparator": ",",
      "DecimalSeparator": ".",
      "SymbolOnLeft": true,"SpaceBetweenAmountAndSymbol": true,"RoundingCoefficient": "DecimalDigits": 2}]}
