using System;
using System.Collections.Generic;

namespace DeThi210702.Models;

public partial class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public string? Address { get; set; }

    public double? Amount { get; set; }

    public string? Description { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
