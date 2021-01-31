using System;
using System.Collections.Generic;

#nullable disable

namespace MMT.CustomerOrder.Models
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public bool? Returnable { get; set; }

        public virtual Order Order { get; set; }
        public Product Product { get; set; }
    }
}
