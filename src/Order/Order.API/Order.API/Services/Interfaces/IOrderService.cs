﻿namespace Order.API.Services.Interfaces;

public interface IOrderService
{
    Task<OrderDto> CreateOrder(int subId);
    Task<IEnumerable<OrderDto>> GetOrders(int subId);
    Task DeleteOrder(int id);
}