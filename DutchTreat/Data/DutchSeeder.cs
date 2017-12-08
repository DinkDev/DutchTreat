using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    using System.IO;
    using Entities;
    using Microsoft.AspNetCore.Hosting;
    using Newtonsoft.Json;

    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hosting;

        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            // create DB is not already there
            _ctx.Database.EnsureCreated();

            if (!_ctx.Products.Any())
            {
                // need to add sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json).ToList();

                _ctx.Products.AddRange(products);

                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                _ctx.Orders.Add(order);

                _ctx.SaveChanges();
            }
        }
    }
}
