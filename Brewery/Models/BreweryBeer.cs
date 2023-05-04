using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class BreweryBeer
{
    public int IdBreweryBeer { get; set; }

    public int BreweryId { get; set; }

    public int BeerId { get; set; }

    public bool IsDeleted { get; set; }
}
