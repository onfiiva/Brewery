using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class Beer
{
    public int IdBeer { get; set; }

    public string NameBeer { get; set; } = null!;

    public DateTime ProductionTime { get; set; }

    public DateTime Term { get; set; }

    public int BeerTypeId { get; set; }

    public bool IsDeleted { get; set; }
}
