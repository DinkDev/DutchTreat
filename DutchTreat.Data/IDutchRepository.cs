namespace DutchTreat.Data
{
    using System.Collections.Generic;
    using Entities;

    public interface IDutchRepository
    {
        void AddEntity(object item);

        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);

        bool SaveAll();
    }
}