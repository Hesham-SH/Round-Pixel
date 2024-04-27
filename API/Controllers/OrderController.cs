using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class OrderController : BaseApiController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
        _orderService.AssociateMediatorConnection(Mediator);
    }

    [Authorize(Policy = "RequireUserRole")]
    [HttpPost("CreateNewOrder")]
    public async Task<ActionResult<string>> CreateNewOrder(OrderDTO orderToAdd)
    {
        return await _orderService.Add(orderToAdd);
    }

    [Authorize(Policy = "RequireUserRole")]
    [HttpGet("GetOrdersByUserId/{userId:Guid")]
    public async Task<ActionResult<List<OrderDTO>>> GetOrdersByUserId(Guid userId)
    {
        return await _orderService.GetOrdersByUserIdAsync(userId);
    }

    [Authorize(Policy = "RequireUserRole")]
    [HttpGet("GetOrderDetailsByOrderId/{orderId:Guid}")]
    public async Task<ActionResult<List<OrderDetailsDTO>>> GetOrderDetailsByOrderId(Guid orderId)
    {
        return await _orderService.GetOrderDetailsByOrderIdAsync(orderId);
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut("CloseOrder")]
    public async Task<ActionResult<string>> CloseOrder(Guid orderId)
    {
        return await _orderService.CloseOrderAsync(orderId);
    }
}
