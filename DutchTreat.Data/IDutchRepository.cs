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
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        Order GetOrderById(string username, int id);

        bool SaveAll();
    }
}