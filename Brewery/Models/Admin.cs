using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class Admin
{
    public int IdAdmin { get; set; }

    public string PhoneAdmin { get; set; } = null!;

    public string LoginAdmin { get; set; } = null!;

    public string PasswordAdmin { get; set; } = null!;

    public int BreweryId { get; set; }

    public string? PasswordSalt { get; set; }

    public bool IsDeleted { get; set; }
}
