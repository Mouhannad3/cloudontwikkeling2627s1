using System;

namespace ShipIt.PriceQuote.Domain;

public class ShipItExceptions : Exception
{
    public class FromCountryWeightException(string message) : Exception(message);

}
