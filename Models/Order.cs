using System;
using System.Collections.Generic;



namespace MMT.CustomerOrder.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryExpected { get; set; }
        public bool? ContainsGift { get; set; }
        public string ShippingMode { get; set; }
        public string OrderSource { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
