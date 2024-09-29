using System;
using System.Collections.Generic;

namespace ModelDto.Models;

public partial class Order
{
    public string Id { get; set; } = null!;

    public string? Codice { get; set; }

    public string? Customername { get; set; }

    public string? Customermobile { get; set; }

    public ulong? IsCompleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
