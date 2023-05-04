using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class Cheque
{
    public int IdCheque { get; set; }

    public int UserId { get; set; }

    public int Sum { get; set; }

    public DateTime TimeOrder { get; set; }

    public bool IsDeleted { get; set; }
}
