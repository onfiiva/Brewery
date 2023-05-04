using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class Subscription
{
    public int IdSubscription { get; set; }

    public string NameSubscription { get; set; } = null!;

    public bool IsDeleted { get; set; }
}
