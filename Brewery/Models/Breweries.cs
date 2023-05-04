using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class Breweries
{
    public int IdBrewery { get; set; }

    public string NameBrewery { get; set; } = null!;

    public string AddressBrewery { get; set; } = null!;

    public bool IsDeleted { get; set; }
}
