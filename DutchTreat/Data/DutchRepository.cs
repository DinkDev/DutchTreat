namespace DutchTreat.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    using Entities;

    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
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
