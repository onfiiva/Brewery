using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class Token
{
    public int IdToken { get; set; }

    public string TokenValue { get; set; } = null!;

    public DateTime TokenDateTime { get; set; }
}
