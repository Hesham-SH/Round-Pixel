using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using Application.Items;
using Application.Orders;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IMapper _mapper;
        private readonly BasicCurrency _baseCurrency;
        private readonly Discount _discount;
        private readonly ICachingService _cachingService;

        public OrderService(IMapper mapper, IOptions<Discount> discount, IOptions<BasicCurrency> baseCurrency, ICachingService cachingService)
        {
            _cachingService = cachingService;
            _mapper = mapper;
            _discount = discount.Value;
            _baseCurrency = baseCurrency.Value;
        }

        public async Task<List<OrderDetailsDTO>> GetOrderDetailsByOrderIdAsync(Guid orderId)
        {
            var orderDetailsList = await _mediator.Send(new GetOrderDetails.Query{ OrderId = orderId});
            var orderDetailsDTOs = _mapper.Map<List<OrderDetails>, List<OrderDetailsDTO>>(orderDetailsList);
            return orderDetailsDTOs;
        }

        public async Task<List<OrderDTO>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await _mediator.Send(new GetUserOrders.Query{UserId = userId});
            var ordersDTOs = _mapper.Map<List<Order>, List<OrderDTO>>(orders);
            return ordersDTOs;
        }
        public async Task<string> Add(OrderDTO orderToAdd)
        {
            var itemsDTOs = orderToAdd.Items.ToList();

            var items = _mapper.Map<List<ItemDTO>, List<Item>>(itemsDTOs);

            await _mediator.Send(new UpdateItemsAvailableQuantity.Command{Items = items});

            var order = PrepareOrder(orderToAdd);

            var result = await _mediator.Send(new AddOrder.Command{Order = order});

            if(result > 0) return "Order Has Been Added Successfully";
            return "Failed To Add Order To The Database";
        }

        public async Task<string> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteOrder.Command{Id = id});
            if(result > 0) return "Order Has Been Deleted Successfully";
            return "Failed To Delete Order From The Database";
        }


        public Task<OrderDTO> GetSingleOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CloseOrderAsync(Guid orderId)
        {
            var result = await _mediator.Send(new CloseOrder.Command{ OrderId = orderId});
            if(result > 0) return "Order Has Been Closed Successfully !";
            return "Failed To Close Order !";
        }
        private Order PrepareOrder(OrderDTO orderToAdd)
        {
            //check for currency with orderToAdd
            var orderToAddWithUpdatedCurrency = CheckCurrencyCode(orderToAdd);

            //check for discount with orderToAdd
            var orderToAddWithUpdatedCurrencyAndPromoCode = CheckPromoCode(orderToAdd);

            //check order total price
            var orderTotalPrice = CalculateOrderTotalPrice(orderToAdd);

            var order = new Order 
            {
                RequestDate = DateTime.Now,
                CloseDate = DateTime.Now.AddDays(7),
                Status = "Open",
                DiscountPromoCode =  orderToAddWithUpdatedCurrencyAndPromoCode.DiscountPromoCode,
                DiscountValue =  orderToAddWithUpdatedCurrencyAndPromoCode.DiscountValue,
                TotalPrice = orderTotalPrice - orderToAddWithUpdatedCurrencyAndPromoCode.DiscountValue,
                ExchangeRate = orderToAddWithUpdatedCurrency.ExchangeRate,
                ForeignPrice = orderTotalPrice / orderToAddWithUpdatedCurrency.ExchangeRate,
                CurrencyCode = orderToAddWithUpdatedCurrency.CurrencyCode,
                CustomerId = orderToAdd.CustomerId,
            };

            order.OrderDetails = orderToAdd.Items.Select(item => new OrderDetails
            {
                OrderId = order.Id,
                ItemId = item.Id,
                ItemPrice = item.Price,
                Quantity = item.Quantity,
                TotalPrice = item.Price * item.Quantity
            }).ToList();
            
            return order;
        }

       

        private OrderDTO CheckCurrencyCode(OrderDTO orderToAdd)
        {
            var currencyCodeValueInCache = _cachingService.GetData<string>(orderToAdd.CurrencyCode);
            if(orderToAdd.CurrencyCode is not null && currencyCodeValueInCache is not null)
            {
                orderToAdd.ExchangeRate = decimal.Parse(currencyCodeValueInCache);
            }
            else
            {
                orderToAdd.CurrencyCode = _baseCurrency.Code;
                orderToAdd.ExchangeRate = _baseCurrency.Exchange;
            }
            return orderToAdd;
        }

        private OrderDTO CheckPromoCode(OrderDTO orderToAdd)
        {
            if(orderToAdd.DiscountPromoCode is not null && orderToAdd.DiscountPromoCode == _discount.PromoCode)
            {
                orderToAdd.DiscountValue = _discount.Value;
            }
            return orderToAdd;
        }
        
        private decimal CalculateOrderTotalPrice(OrderDTO orderToAdd)
        {
            decimal totalPrice = 0;
            foreach(var itemDTO in orderToAdd.Items)
            {
                totalPrice += itemDTO.Price * itemDTO.Quantity;
            }
            return totalPrice;
        }
    }
}