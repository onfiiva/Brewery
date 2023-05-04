using System;
using System.Collections.Generic;

namespace BreweryAPI.Models;

public partial class Supplier
{
    public int IdSupplier { get; set; }

    public string NameSupplier { get; set; } = null!;

    public string PhoneSupplier { get; set; } = null!;

    public string AddressSupplier { get; set; } = null!;

    public bool IsDeleted { get; set; }
}
