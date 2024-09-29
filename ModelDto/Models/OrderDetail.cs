using System;
using System.Collections.Generic;

namespace ModelDto.Models;

public partial class OrderDetail
{
    public string Id { get; set; } = null!;

    public string OrderId { get; set; } = null!;

    public string? Name { get; set; }

    public int? Qty { get; set; }

    public double? Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
