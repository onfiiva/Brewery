using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class IngridientsType
{
    public int IdIngridientType { get; set; }

    public string NameIngridientType { get; set; } = null!;

    public bool IsDeleted { get; set; }
}
