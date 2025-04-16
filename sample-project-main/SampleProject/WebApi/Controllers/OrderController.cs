using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Core.Services.Orders;
using WebApi.Models.Orders;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;

        public OrderController(ICreateOrderService createOrderService, IDeleteOrderService deleteOrderService, IGetOrderService getOrderService, IUpdateOrderService updateOrderService)
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var validationErrors = _updateOrderService.ValidateModelInputs(model.UserId, model.Date, model.ProductIds);
            if (validationErrors.Any())
            {
                return BadRequestResponse(string.Join(",", validationErrors));
            }
            var existingOrder = _getOrderService.GetOrder(orderId);
            if (existingOrder != null)
            {
                return BadRequestResponse("Record with the same ID already exists");
            }
            var order = _createOrderService.Create(orderId, model.UserId, model.Date, model.ProductIds);
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }

            var validationErrors = _updateOrderService.ValidateModelInputs(model.UserId, model.Date, model.ProductIds);
            if (validationErrors.Any())
            {
                return BadRequestResponse(string.Join(",", validationErrors));
            }
            _updateOrderService.Update(order, model.UserId, model.Date, model.ProductIds);
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            _deleteOrderService.Delete(order);
            return Found();
        }

        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            return Found(new OrderData(order));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders(int skip, int take, Guid? userId = null, DateTime? date = null, Guid? productId = null)
        {
            var orders = _getOrderService.GetOrders(userId, date, productId)
                                       .Skip(skip).Take(take)
                                       .Select(q => new OrderData(q))
                                       .ToList();
            return Found(orders);
        }
    }
}