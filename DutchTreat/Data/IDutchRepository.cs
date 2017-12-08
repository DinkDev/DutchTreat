namespace DutchTreat.Data
{
    using System.Collections.Generic;
    using Entities;

    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();

        IEnumerable<Product> GetProductsByCategory(string category);

        bool SaveAll();
    }
}