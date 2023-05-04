using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class BeerType
{
    public int IdBeerType { get; set; }

    public string NameBeerType { get; set; } = null!;

    public bool IsDeleted { get; set; }
}
