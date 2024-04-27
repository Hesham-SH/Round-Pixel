using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IOrderService : IBaseService
{
    //Get All Order Paginated
    Task<List<OrderDTO>> GetOrdersByUserIdAsync(Guid id);
    Task<OrderDTO> GetSingleOrderByIdAsync(Guid id);
    Task<List<OrderDetailsDTO>> GetOrderDetailsByOrderIdAsync(Guid orderId);
    Task<string> Delete(Guid id);
    Task<string> Add(OrderDTO orderToAdd);
    Task<string> CloseOrderAsync(Guid orderId);
}
