using Microsoft.AspNetCore.Mvc;
using MMT.CustomerOrder.Api;
using MMT.CustomerOrder.Exceptions;
using MMT.CustomerOrder.Models;
using System;
using System.Threading.Tasks;

namespace MMT.CustomerOrder.Controllers
{
    /// <summary>
    /// This class handle order request
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController
    {
        private IOrderApi _orderApi;
        public OrderController(IOrderApi orderApi)
        {
            if (orderApi == null)
                throw new ArgumentNullException();
            _orderApi = orderApi;
        }
        [HttpPost("GetUserLatestOrder")]
        public async Task<IActionResult> GetUserLatestOrder(OrderParam orderParam)
        {
            try
            {
                return new JsonResult(await _orderApi.GetLatestOrderAsync(orderParam));
            }
            catch 
            {
                return new BadRequestResult();
            }

        }
        
    }
}
