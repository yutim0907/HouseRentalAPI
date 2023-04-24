using System;
using System.Collections.Generic;

namespace HouseRentalAPI.Models;

public partial class HouseRentalPost
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Address { get; set; } = null!;

    public decimal Price { get; set; }

    public int LeaseDuration { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }
}
