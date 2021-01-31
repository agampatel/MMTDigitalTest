using MMT.CustomerOrder.Core;
using MMT.CustomerOrder.Exceptions;
using MMT.CustomerOrder.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMT.CustomerOrder.Api
{
    public interface IOrderApi
    {
        Task<OrderDetails> GetLatestOrderAsync(OrderParam orderParam);
    }
    /// <summary>
    /// Api handle order request
    /// </summary>
    public class OrderApi:IOrderApi
    {
        private IOrderRepository _orderRepository;
        private IUserApi _customerApi;

        public OrderApi(IOrderRepository orderRepository,IUserApi customerApi)
        {
            if (orderRepository == null || customerApi==null)
                throw new ArgumentNullException();

            _orderRepository = orderRepository;
            _customerApi = customerApi;
        }
        /// <summary>
        /// Get latest by parameter value <paramref name="orderParam"/>
        /// </summary>
        /// <param name="orderParam"></param>
        /// <returns><see cref="OrderDetails"/></returns>
        public async Task<OrderDetails> GetLatestOrderAsync(OrderParam orderParam)
        {
            if (string.IsNullOrWhiteSpace(orderParam.CustomerId) || string.IsNullOrWhiteSpace(orderParam.User))
                throw new ArgumentNullException();

            var customer = _customerApi.GetUserAsync(orderParam.User);
            var order = _orderRepository.GetLatestOrderAsync(orderParam.CustomerId);

            await Task.WhenAll(customer, order);

            if (customer.Result == null)
                throw new AppException($"Invalid user email {orderParam.User}");
            
            //Throw an exception if the customer id doesn't match with the value in the parameter
            if (customer.Result.CustomerId != orderParam.CustomerId)
                throw new AppException("Customer id doesn't match with customer id in order");

            
            return PopulateOrderDetails(customer.Result,order.Result);
            
        }
        private OrderDetails PopulateOrderDetails(Customer customer,Order order)
        {
            var result = new OrderDetails();
            result.Customer = new CustomerDetails() { FirstName = customer.FirstName, LastName = customer.LastName };
            result.Order = new Models.CustomerOrder();
            if (order != null)
            {
                result.Order.DeliveryAddress = $"{customer.HouseNumber} {customer.Street}, {customer.Town}, {customer.Postcode}";
                result.Order.OrderDate = order.OrderDate.HasValue ? order.OrderDate.Value.ToString("dd-MMM-yyyy") : string.Empty;
                result.Order.OrderNumber = order.OrderId;
                result.Order.DeliveryExpected = order.DeliveryExpected.HasValue ? order.DeliveryExpected.Value.ToString("dd-MMM-yyyy") : string.Empty;
                if (order.OrderItems != null)
                {
                    bool isGift = order.ContainsGift.GetValueOrDefault(false);
                    result.Order.OrderItems = new List<CustomerOrderItem>();
                    foreach (OrderItem orderItem in order.OrderItems)
                    {
                        result.Order.OrderItems.Add(new CustomerOrderItem() { PriceEach = orderItem.Price.GetValueOrDefault(0), Product = isGift ? "Gift" : orderItem.Product.ProductName, Quantity = orderItem.Quantity });
                    }
                }
            }
            return result;

        }
    }
}
