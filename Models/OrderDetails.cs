using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMT.CustomerOrder.Models
{

    
    public class OrderDetails
    {
        public CustomerDetails Customer { get; set; }
        public CustomerOrder Order { get; set; }
    }
    public class CustomerDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class CustomerOrder
    {
        public int OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public List<CustomerOrderItem> OrderItems { get; set; }
        public string DeliveryExpected { get; set; }
    }
    public class CustomerOrderItem
    {
        public string Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? PriceEach { get; set; }
    }
    
}

