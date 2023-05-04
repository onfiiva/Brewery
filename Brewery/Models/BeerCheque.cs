using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class BeerCheque
{
    public int IdBeerCheque { get; set; }

    public int ChequeId { get; set; }

    public int BeerId { get; set; }

    public bool IsDeleted { get; set; }
}
