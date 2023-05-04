using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string UserPhone { get; set; } = null!;

    public string LoginUser { get; set; } = null!;

    public string PasswordUser { get; set; } = null!;

    public int SubscriptionId { get; set; }

    public string? PasswordSalt { get; set; }

    public bool IsDeleted { get; set; }
}
