﻿using Microsoft.EntityFrameworkCore;
using Order.API.Data;
using Order.API.Models.Dto;
using Order.API.Repositories.Interfaces;

namespace Order.API.Repositories;

public class OrderRepository : IOrderRepository
{
    private AppDbContext _appDbContext;

    public OrderRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    
    
    public async Task<Models.Order> CreateOrder(Models.Order order)
    {
        foreach (var shoppingCart in order.ShoppingCarts)
        {
            var existingShoppingCart = _appDbContext.ShoppingCarts
                .Include(sh => sh.Product).AsNoTracking()
                .FirstOrDefault(sh => sh.Id == shoppingCart.Id);

            if (existingShoppingCart == null)
            {
                _appDbContext.ShoppingCarts.Add(shoppingCart);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                
                // Update existingShoppingCart properties
               // _appDbContext.Attach(existingShoppingCart);
                shoppingCart.Product = existingShoppingCart.Product; // Set the relationship
            }

            var existingProduct = _appDbContext.Products.
                AsNoTracking()
                .FirstOrDefault(p => p.ProductId == shoppingCart.Product.ProductId);

            if (existingProduct == null)
            {
                _appDbContext.Products.Add(shoppingCart.Product);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
               // _appDbContext.Attach(existingProduct);
                shoppingCart.Product = existingProduct; // Set the relationship
            }
        }

        _appDbContext.Orders.Add(order);

        var result = await _appDbContext.SaveChangesAsync();

        if (result == 0)
        {
            throw new RepositoryException("Order didn't save changes!");
        }

        return order;
    }

    public IQueryable<Models.Order> FindAll()
    {
        var result = _appDbContext.Set<Models.Order>().AsQueryable().AsNoTracking()
            .Include(p => p.ShoppingCarts)
            .ThenInclude(sh => sh.Product);
        if (result == null)
        {
            throw new RepositoryException("There are no Orders");
        }

        return result;
    }
}