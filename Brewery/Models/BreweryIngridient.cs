using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class BreweryIngridient
{
    public int IdBreweryIngridients { get; set; }

    public int BreweryId { get; set; }

    public int IngridientId { get; set; }

    public bool IsDeleted { get; set; }
}
