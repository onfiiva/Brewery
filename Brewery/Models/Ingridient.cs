using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class Ingridient
{
    public int IdIngridient { get; set; }

    public string NameIngridient { get; set; } = null!;

    public int IngridientTypeId { get; set; }

    public int AdminId { get; set; }

    public int SupplierId { get; set; }

    public bool IsDeleted { get; set; }
}
