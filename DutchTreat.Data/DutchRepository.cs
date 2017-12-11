namespace DutchTreat.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object entity)
        {
            _ctx.Add(entity);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");
                return _ctx.Products
                           .OrderBy(p => p.Title)
                           .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetAllProducts)} failed: {ex}");
                return null;
            }
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product) // look inside OrderItem
                    .ToList();
            }
            else
            {
                return _ctx.Orders.ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == username)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product) // look inside OrderItem
                    .ToList();
            }
            else
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == username)
                    .ToList();
            }
        }

        public Order GetOrderById(string username, int id)
        {
            return _ctx.Orders
                .Include(o => o.Items)    // look inside OrderItem
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o => o.Id == id && o.User.UserName == username);
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            try { 
            return _ctx.Products
                       .Where(p => p.Category == category)
                       .OrderBy(p => p.Title)
                       .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetProductsByCategory)} failed: {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SaveAll)} failed: {ex}");
                return false;
            }
        }
    }
}
