using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class UserHistory
{
    public int IdUserHistory { get; set; }

    public string? UserHistory1 { get; set; }

    public int UserId { get; set; }

    public DateTime? CreateRecord { get; set; }

    public DateTime? ChangeRecord { get; set; }

    public bool IsDeleted { get; set; }
}
