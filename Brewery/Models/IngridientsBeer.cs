using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class IngridientsBeer
{
    public int IdUsersBeer { get; set; }

    public int IngridientId { get; set; }

    public int BeerId { get; set; }

    public bool IsDeleted { get; set; }
}
